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
    /// ボスの体力を表示するやつ
    /// </summary>
    [System.Serializable]
    public class BossHealthBar
    {
        [SerializeField] SliderController _slider;
        /// <summary>Sliderの表示非表示</summary>
        public bool SetActive { set => _slider.gameObject.SetActive(value); }

        public void Setup(int maxHp, System.IObservable<int> currentHpObservable, Component component)
        {
            if (!_slider)
            {
                Debug.LogWarning("Sliderがありません");
                return;
            }
            _slider.Setup(maxHp, maxHp);
            currentHpObservable
                .Subscribe(i => _slider.Value = i)
                .AddTo(component);
        }
    }
}
