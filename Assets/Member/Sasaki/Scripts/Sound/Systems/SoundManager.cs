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

    void Awake()
    {
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

    public void PlaySound(SoundType type, string path)
    {
        SoundDataAsset asset = _soundDataAssetList.Find(s => s.SoundType == type);
        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        Sounder sounder = _pool.UseRequest(out System.Action action);
        action += () => sounder.SetData(data, asset.VolumeType);

        action.Invoke();
    }
}
