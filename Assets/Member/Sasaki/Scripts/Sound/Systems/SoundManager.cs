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

    Pool<Sounder> _pool = new Pool<Sounder>();
    Sounder _bgmSounder;

    static SoundManager Instance = null;

    public static bool IsStopAllSound
    { 
        get
        {
            Debug.Log(Instance._pool.CurrentUseCount);
            return Instance._pool.CurrentUseCount <= 0;
        }
    }

    void Awake()
    {
        Instance = this;

        _pool
            .SetMono(_sounderPrefab, _poolCount)
            .IsSetParent(transform)
            .CreateRequest();
    }

    void Update()
    {
        SoundMasterData.MasterValume = _masterVolume;
        SoundMasterData.BGMVolume = _bgmVolume;
        SoundMasterData.SEVoume = _seVolume;
    }

    public static void PlayRequest(SoundType type, string path)
    {
        if (Instance == null) return;
      
        SoundDataAsset asset = Instance._soundDataAssetList.Find(s => s.SoundType == type);

        if (asset == null)
        {
            Debug.Log("サウンドアセットデータが存在しません");
            return;
        }

        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        if (data == null)
        {
            Debug.Log("サウンドデータが存在しません");
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
        if (Instance == null) return;

        SoundDataAsset asset = Instance._soundDataAssetList.Find(s => s.SoundType == type);
        SoundDataAsset.SoundData data = asset.GetSoundDataRandom(containPath);

        if (data == null)
        {
            Debug.Log("サウンドデータが存在しません");
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
        if (Instance._bgmSounder != null)
        {
            Instance._bgmSounder.OnStop();
        }
    }
}
