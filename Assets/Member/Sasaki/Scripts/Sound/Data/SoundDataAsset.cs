using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public enum VolumeType
    {
        BGM,
        SE,
    }

    public enum SoundType
    {
        BGM,
        SEEnemy,
        SEPlayer,
        SEOther,
    }

    [CreateAssetMenu(fileName = "SoundDataAsset_[Name]")]
    public class SoundDataAsset : ScriptableObject
    {
        [System.Serializable]
        public class SoundData
        {
            [SerializeField] string _path;
            [SerializeField] AudioClip _clip;
            [SerializeField, Range(0, 1)] float _volume;
            [SerializeField] bool _isLoop;

            public string Path => _path;
            public AudioClip Clip => _clip;
            public float Volume => _volume;
            public bool IsLoop => _isLoop;
        }

        [SerializeField] VolumeType _volumeType;
        [SerializeField] SoundType _soundType;
        [SerializeField] List<SoundData> _soundDataList;

        public VolumeType VolumeType => _volumeType;
        public SoundType SoundType => _soundType;
        public SoundData GetSoundData(string path) => _soundDataList.Find(s => s.Path == path);
    }

}