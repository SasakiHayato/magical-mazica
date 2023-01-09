using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// スライダーの管理クラス
/// </summary>
public class SliderController : MonoBehaviour
{
    [SerializeField] Slider _mainSlider;
    [SerializeField] Slider _subSlider;
    [SerializeField] float _duration;

    /// <summary>スライダーの値変更</summary>
    public int Value { get => (int)_mainSlider.value; set => _mainSlider.value = value; }

    public void Setup(int maxValue, int initValue)
    {
        _mainSlider.maxValue = maxValue;
        _mainSlider.value = initValue;
        _subSlider.maxValue = maxValue;
        _subSlider.value = initValue;

        _mainSlider.onValueChanged
            .AsObservable()
            .Subscribe(i =>
            {
                if (_subSlider.value >= i)
                {
                    DOTween.To(() => _subSlider.value, (x) => _subSlider.value = x, i, _duration);
                }
                else
                {
                    _subSlider.value = i;
                }
            })
            .AddTo(this);
    }
}
