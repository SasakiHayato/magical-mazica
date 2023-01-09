using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 秋元のサンプルクラス
/// </summary>
public class ASample : MonoBehaviour
{
    [SerializeField] SliderController _sliderController;

    private async void Start()
    {
        _sliderController.Setup(100, 100);

        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _sliderController.Value -= 5;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _sliderController.Value -= 10;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _sliderController.Value -= 10;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _sliderController.Value -= 20;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _sliderController.Value -= 20;
    }
}
