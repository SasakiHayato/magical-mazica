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
    /// <summary>�p���b��</summary>
    public int Time { get; set; }
    /// <summary>���ʏI���C�x���g</summary>
    public IObservable<Unit> EndEvent => _endEvent;
    /// <summary>
    /// ���ʔ���
    /// </summary>
    /// <param name="method">���ʔ������ɌĂ΂��֐�</param>
    public abstract void Effect(Action<int> method, Component component);
    /// <summary>
    /// �J�E���g�_�E��
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
    /// <summary>��</summary>
    Posion,
}
/// <summary>
/// ��Ԉُ�]���̎��ɕ]�������l
/// </summary>
public enum EffectEvaluationStatType
{
    Health,
}
/// <summary>
/// ��Ԉُ�]���^�C�~���O
/// </summary>
public enum EffectTiming
{
    EverySecond,
    OneTimeOnly,
}