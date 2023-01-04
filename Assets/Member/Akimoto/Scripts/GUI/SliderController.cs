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
    [SerializeField] Slider _slider;
    [SerializeField] Image _fillImage;
    [SerializeField] Image _animationImage;
    [SerializeField] Image _backGroundImage;
    /// <summary>塗りつぶす範囲の色を変える</summary>
    public Color SetFillColor { set => _fillImage.color = value; }
    /// <summary>スライダーが減る際にアニメーションさせる部分の色を変える</summary>
    public Color SetAnimationImageColor { set => _animationImage.color = value; }
    /// <summary>背景の色を変える</summary>
    public Color SetBackGroundColor { set => _backGroundImage.color = value; }
    public int SetMaxValue { set => _slider.maxValue = value; }
    public int SetCurrentValue
    {
        set
        {
            _slider.value = value;
        }
    }
}
