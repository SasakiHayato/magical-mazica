using System.Collections.Generic;
using UnityEngine;
using SoundSystem;
using ObjectPool;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounder _sounderPrefab;
    [SerializeField] int _poolCount;
    [SerializeField] List<SoundDataAsset> _soundDataAssetList;
    [SerializeField, Range(0, 1)] float _masterVolume;
    [SerializeField, Range(0, 1)] float _bgmVolume;
    [SerializeField, Range(0, 1)] float _seVolume;

    float _bgmDataVolume = 0;

    Pool<Sounder> _pool = new Pool<Sounder>();
    Sounder _bgmSounder;

    AudioSource _audioSource;

    static SoundManager Instance = null;

    public static bool IsStopAllSound => Instance._pool.CurrentUseCount <= 0;
   
    void Awake()
    {
        Instance = this;

        _pool
            .SetMono(_sounderPrefab, _poolCount)
            .IsSetParent(transform)
            .CreateRequest();

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void Update()
    {
        SoundMasterData.MasterValume = _masterVolume;
        SoundMasterData.BGMVolume = _bgmVolume;
        SoundMasterData.SEVoume = _seVolume;

        _audioSource.volume = SetVolume(VolumeType.BGM, _bgmDataVolume);
    }

    float SetVolume(VolumeType volumeType, float dataVolume)
    {
        float volume = SoundMasterData.MasterValume * dataVolume;

        switch (volumeType)
        {
            case VolumeType.BGM:
                volume = volume * SoundMasterData.BGMVolume;
                break;
            case VolumeType.SE:
                volume = volume * SoundMasterData.SEVoume;
                break;
        }

        return volume;
    }

    public static void PlayBGM(string path)
    {
        if (Instance == null) return;

        SoundDataAsset asset = Instance._soundDataAssetList.Find(s => s.SoundType == SoundType.BGM);

        if (asset == null)
        {
            Debug.Log("�T�E���h�A�Z�b�g�f�[�^�����݂��܂���");
            return;
        }

        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        if (data == null)
        {
            Debug.Log("�T�E���h�f�[�^�����݂��܂���");
            return;
        }

        Instance._audioSource.clip = data.Clip;
        Instance._bgmDataVolume = data.Volume;
        Instance._audioSource.loop = data.IsLoop;
        Instance._audioSource.Play();
    }

    public static void PlayRequest(SoundType type, string path)
    {
        if (Instance == null || type == SoundType.BGM) return;
      
        SoundDataAsset asset = Instance._soundDataAssetList.Find(s => s.SoundType == type);

        if (asset == null)
        {
            Debug.Log("�T�E���h�A�Z�b�g�f�[�^�����݂��܂���");
            return;
        }

        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        if (data == null)
        {
            Debug.Log("�T�E���h�f�[�^�����݂��܂���");
            return;
        }

        Sounder sounder = Instance._pool.UseRequest(out System.Action action);
        action += () => sounder.SetData(data, asset.VolumeType);

        action.Invoke();

        if (type == SoundType.BGM)
        {
            if (Instance._bgmSounder != null)
            {
                StopBGM();
            }

            Instance._bgmSounder = sounder;
        }
    }

    public static void PlayRequestRandom(SoundType type, string containPath)
    {
        if (Instance == null || type == SoundType.BGM) return;

        SoundDataAsset asset = Instance._soundDataAssetList.Find(s => s.SoundType == type);
        SoundDataAsset.SoundData data = asset.GetSoundDataRandom(containPath);

        if (data == null)
        {
            Debug.Log("�T�E���h�f�[�^�����݂��܂���");
            return;
        }

        Sounder sounder = Instance._pool.UseRequest(out System.Action action);
        action += () => sounder.SetData(data, asset.VolumeType);

        action.Invoke();

        if (type == SoundType.BGM)
        {
            if (Instance._bgmSounder != null)
            {
                StopBGM();
            }

            Instance._bgmSounder = sounder;
        }
    }

    public static void StopBGM()
    {
        //if (Instance._bgmSounder != null)
        //{
        //    Instance._bgmSounder.OnStop();
        //}

        if (Instance._audioSource != null)
        {
            Instance._audioSource.Stop();
        }
    }
}
