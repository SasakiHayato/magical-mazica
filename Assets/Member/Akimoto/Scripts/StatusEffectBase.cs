using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class StatusEffectBase : MonoBehaviour
{
    /// <summary>�p���b��</summary>
    public float Time { get; set; }
    /// <summary>�߂�l</summary>
    public EffectReturnValue EffectReturnValue { get; set; }
    /// <summary>
    /// ���ʔ���
    /// </summary>
    /// <param name="method">�]�����鐔�l</param>
    /// <param name="evaluationStatType">�]������</param>
    /// <returns></returns>
    public abstract int Effect(System.Func<int> method, EffectEvaluationStatType evaluationStatType);
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
/// ��Ԉُ�̖߂�l
/// </summary>
public enum EffectReturnValue
{
    Health,
}