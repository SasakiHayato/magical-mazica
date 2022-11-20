using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectDataAsset")]
public class EffectDataAsset : ScriptableObject
{
    [System.Serializable]
    public class EffectData
    {
        [SerializeField] string _path;
        [SerializeField] Effect _effect;

        public string Path => _path;
        public Effect Effect => _effect;
    }

    [SerializeField] List<EffectData> _effectDataList;

    public List<EffectData> EffectDataList => _effectDataList;
}
