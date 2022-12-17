using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class StatusEffectBase : MonoBehaviour
{
    protected Subject<Unit> _endEvent = new Subject<Unit>();
    protected IConnectableObservable<int> _countDown;
    /// <summary>継続秒数</summary>
    public int Time { get; set; }
    /// <summary>効果終了イベント</summary>
    public IObservable<Unit> EndEvent => _endEvent;
    /// <summary>
    /// 効果発動
    /// </summary>
    /// <param name="method">効果発動時に呼ばれる関数</param>
    public abstract void Effect(Action<int> method, Component component);
    /// <summary>
    /// カウントダウン
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    protected IObservable<int> CreateCountDownStream(int time)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select(x => (int)(time - x))
            .TakeWhile(x => x > 0);
    }
}
public enum StatusEffectID
{
    /// <summary>毒</summary>
    Posion,
}
/// <summary>
/// 状態異常評価の時に評価される値
/// </summary>
public enum EffectEvaluationStatType
{
    Health,
}
/// <summary>
/// 状態異常評価タイミング
/// </summary>
public enum EffectTiming
{
    EverySecond,
    OneTimeOnly,
}