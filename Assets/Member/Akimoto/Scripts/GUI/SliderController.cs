using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �X���C�_�[�̊Ǘ��N���X
/// </summary>
public class SliderController : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] Image _fillImage;
    [SerializeField] Image _animationImage;
    [SerializeField] Image _backGroundImage;
    /// <summary>�h��Ԃ��͈͂̐F��ς���</summary>
    public Color SetFillColor { set => _fillImage.color = value; }
    /// <summary>�X���C�_�[������ۂɃA�j���[�V���������镔���̐F��ς���</summary>
    public Color SetAnimationImageColor { set => _animationImage.color = value; }
    /// <summary>�w�i�̐F��ς���</summary>
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
