using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StatusEffects { }

public class Poison : StatusEffectBase
{
    public int Damage { get; set; }
    public override void Effect(Action<int> damageMethod, Component component)
    {
        _countDown = CreateCountDownStream(Time).Publish();
        _countDown.Connect();
        _countDown
            .AsObservable()
            .Subscribe(_ =>
            {
                damageMethod(Damage);
            }, () =>
            {
                _endEvent.OnNext(Unit.Default);
            })
            .AddTo(component);
    }
}
