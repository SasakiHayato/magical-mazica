using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

/// <summary>
/// �A�C�e�����擾������@�\�̕���
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class AcquisitionDropObject : MonoBehaviour
{
    private Action _action;
    public Action SetAction { set => _action = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _action();
    }
}
