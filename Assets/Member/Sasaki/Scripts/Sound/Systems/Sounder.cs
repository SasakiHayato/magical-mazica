using ObjectPool;
using ObjectPool.Event;
using UnityEngine;

namespace SoundSystem
{
    public class Sounder : MonoBehaviour, IPool, IPoolEvent
    {
        float _dataVolume;
        bool _isStop;

        AudioSource _audioSource;
        VolumeType _volumeType;

        bool IPoolEvent.IsDone => _isStop;

        void IPool.Setup(Transform parent)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _isStop = false;
        }

        public void SetData(SoundDataAsset.SoundData data, VolumeType volumeType)
        {
            _volumeType = volumeType;
            _dataVolume = data.Volume;

            _audioSource.clip = data.Clip;
            _audioSource.volume = SetVolume(volumeType, data.Volume);
            _audioSource.loop = data.IsLoop;

            _audioSource.Play();
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

        bool IPool.Execute()
        {
            _audioSource.volume = SetVolume(_volumeType, _dataVolume);
            return !_audioSource.isPlaying || _isStop;
        }

        public void OnStop()
        {
            _isStop = true;
        }
    }
}
