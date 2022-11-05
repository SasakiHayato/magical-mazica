using UnityEngine;

namespace SoundSystem
{
    public class Sounder : MonoBehaviour, ISound
    {
        float _dataVolume;

        AudioSource _audioSource;
        VolumeType _volumeType;

        public void Setup()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            gameObject.SetActive(false);
        }

        void ISound.SetData(SoundDataAsset.SoundData data, VolumeType volumeType)
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

        bool ISound.OnExecute()
        {
            _audioSource.volume = SetVolume(_volumeType, _dataVolume);
            return !_audioSource.isPlaying;
        }
    }
}
