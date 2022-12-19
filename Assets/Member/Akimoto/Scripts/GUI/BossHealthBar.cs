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
        [SerializeField] Slider _slider;
        /// <summary>Slider�̕\����\��</summary>
        public bool SetActive { set => _slider.gameObject.SetActive(value); }

        public void Setup(int maxHp, System.IObservable<int> currentHpObservable, Component component)
        {
            if (!_slider)
            {
                Debug.LogWarning("Slider������܂���");
                return;
            }
            _slider.maxValue = maxHp;
            _slider.value = maxHp;
            currentHpObservable
                .Subscribe(i => _slider.value = i)
                .AddTo(component);
        }
    }
}
