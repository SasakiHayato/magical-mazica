using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class StatusEffectBase : MonoBehaviour
{
    /// <summary>Œp‘±•b”</summary>
    public float Time { get; set; }
    /// <summary>–ß‚è’l</summary>
    public EffectReturnValue EffectReturnValue { get; set; }
    /// <summary>
    /// Œø‰Ê”­“®
    /// </summary>
    /// <param name="method">•]‰¿‚·‚é”’l</param>
    /// <param name="evaluationStatType">•]‰¿‚·‚é</param>
    /// <returns></returns>
    public abstract int Effect(System.Func<int> method, EffectEvaluationStatType evaluationStatType);
}
public enum StatusEffectID
{
    /// <summary>“Å</summary>
    Posion,
}
/// <summary>
/// ó‘ÔˆÙí•]‰¿‚Ì‚É•]‰¿‚³‚ê‚é’l
/// </summary>
public enum EffectEvaluationStatType
{
    Health,
}
/// <summary>
/// ó‘ÔˆÙí‚Ì–ß‚è’l
/// </summary>
public enum EffectReturnValue
{
    Health,
}