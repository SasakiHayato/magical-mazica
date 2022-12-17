using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StatEffectTest : MonoBehaviour
{
    Poison _poison = new Poison();

    private void Start()
    {
        _poison.Damage = 1;
        _poison.Time = 5;
        _poison.EndEvent.Subscribe(_ => Debug.Log("��Ԉُ�I��"));
        _poison.Effect(AddDamage, this);
    }

    private void AddDamage(int i)
    {
        Debug.Log($"{i}�_���[�W�H�����");
    }
}
