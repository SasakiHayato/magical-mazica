using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoundSystem
{
    public class SoundPool<Sound> where Sound : MonoBehaviour, ISound
    {
        Sound _sound;
        List<Sound> _soundList;
        Transform _parent;

        public SoundPool(Sound sound, Transform parent)
        {
            _sound = sound;
            _parent = parent;
        }

        public void Create(int count)
        {
            _soundList = new List<Sound>();

            for (int index = 0; index < count; index++)
            {
                Sound sound = Object.Instantiate(_sound);
                sound.transform.SetParent(_parent);
                sound.Setup();
                _soundList.Add(sound);
            }
        }

        public void Use(SoundDataAsset.SoundData data, VolumeType volumeType)
        {
            Sound sound = _soundList.FirstOrDefault(s => !s.gameObject.activeSelf);

            if (sound == null)
            {
                Create(10);
                Use(data, volumeType);
                return;
            }

            sound.gameObject.SetActive(true);
            sound.SetData(data, volumeType);
            sound.StartCoroutine(OnProcess(sound));
        }

        IEnumerator OnProcess(Sound sound)
        {
            yield return new WaitUntil(() => sound.OnExecute());
            sound.gameObject.SetActive(false);
        }
    }

}