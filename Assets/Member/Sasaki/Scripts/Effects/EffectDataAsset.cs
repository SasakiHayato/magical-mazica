using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EffectDataAsset")]
public class EffectDataAsset : ScriptableObject
{
    [System.Serializable]
    public class EffectData
    {
        [SerializeField] string _path;
        [SerializeField] Effecter _effecter;

        public string Path => _path;
        public Effecter Effecter => _effecter;
    }

    [SerializeField] List<EffectData> _effectDataList;

    public Effecter GetEffect(string path) => _effectDataList.Find(e => e.Path == path).Effecter;
}
