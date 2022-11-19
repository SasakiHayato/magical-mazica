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

    protected Pool<Sounder> Pool { get; private set; } = new Pool<Sounder>();

    protected List<SoundDataAsset> SoundDataAssetList => _soundDataAssetList;
    protected static SoundManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        Pool
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
        SoundDataAsset asset = Instance.SoundDataAssetList.Find(s => s.SoundType == type);
        SoundDataAsset.SoundData data = asset.GetSoundData(path);

        Sounder sounder = Instance.Pool.UseRequest(out System.Action action);
        action += () => sounder.SetData(data, asset.VolumeType);

        action.Invoke();
    }
}
