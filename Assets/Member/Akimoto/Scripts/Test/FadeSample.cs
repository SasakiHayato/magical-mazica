using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FadeSample : MonoBehaviour
{
    [SerializeField] FadeManager _manager;

    private async void Start()
    {
        _manager.Setup();
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        await _manager.PlayAnimation(FadeAnimationType.Sinple, FadeType.In);
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        await _manager.PlayAnimation(FadeAnimationType.Sinple, FadeType.Out);
    }
}
