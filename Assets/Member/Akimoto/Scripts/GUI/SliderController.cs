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

    public void Setup()
    {
        _subSlider.maxValue = _mainSlider.maxValue;
        _subSlider.value = _mainSlider.value;

        _mainSlider.onValueChanged
            .AsObservable()
            .Subscribe(i =>
            {
                DOTween.To(() => _subSlider.value, (x) => _subSlider.value = x, i, _duration);
            })
            .AddTo(this);
    }
}
