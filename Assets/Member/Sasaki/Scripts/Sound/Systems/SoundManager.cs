using System.Collections.Generic;
using UnityEngine;
using SoundSystem;

namespace SoundSystem
{
    public interface ISound
    {
        void Setup();
        void SetData(SoundDataAsset.SoundData data, VolumeType volumeType);
        bool OnExecute();
    }
}


public class SoundManager : MonoBehaviour
{
    [SerializeField] Sounder _sounderPrefab;
    [SerializeField] int _poolCount;
    [SerializeField] List<SoundDataAsset> _soundDataAssetList;
    [SerializeField, Range(0, 1)] float _masterVolume;
    [SerializeField, Range(0, 1)] float _bgmVolume;
    [SerializeField, Range(0, 1)] float _seVolume;

    SoundPool<Sounder> _pool;

    void Awake()
    {
        _pool = new SoundPool<Sounder>(_sounderPrefab, transform);
        _pool.Create(_poolCount);
    }

    void Update()
    {
        SoundMasterData.MasterValume = _masterVolume;
        SoundMasterData.BGMVolume = _bgmVolume;
        SoundMasterData.SEVoume = _seVolume;
    }

    public void PlaySound(SoundType type, string path)
    {
        SoundDataAsset asset = _soundDataAssetList.Find(s => s.SoundType == type);
        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        _pool.Use(data, asset.VolumeType);
    }
}
