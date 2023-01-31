using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

/// <summary>
/// アイテムを取得させる機能の部分
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class AcquisitionDropObject : MonoBehaviour
{
    [SerializeField] string _tagName;
    private Action _action;
    public string SetTagName { set => _tagName = value; }
    public Action SetAction { set => _action = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_action != null)
        {
            if (collision.CompareTag(_tagName))
            {
                _action();
            }
        }
    }
}
