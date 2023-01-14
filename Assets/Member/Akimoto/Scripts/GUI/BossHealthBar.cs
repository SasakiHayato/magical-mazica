using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace UIManagement
{
    /// <summary>
    /// �{�X�̗̑͂�\��������
    /// </summary>
    [System.Serializable]
    public class BossHealthBar
    {
        [SerializeField] SliderController _slider;
        /// <summary>Slider�̕\����\��</summary>
        public bool SetActive { set => _slider.gameObject.SetActive(value); }

        public void Setup(int maxHp, System.IObservable<int> currentHpObservable, Component component)
        {
            if (!_slider)
            {
                Debug.LogWarning("Slider������܂���");
                return;
            }
            _slider.Setup(maxHp, maxHp);
            currentHpObservable
                .Subscribe(i => _slider.Value = i)
                .AddTo(component);
        }
    }
}
