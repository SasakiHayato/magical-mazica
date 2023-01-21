using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 任意のタグが近くに来たらイベントを発行する
/// </summary>
public class ApproachingTag : MonoBehaviour
{
    [SerializeField] string _tagName;
    private Subject<Unit> _approachEvent = new Subject<Unit>();
    /// <summary>対象が接近してきた際のイベント</summary>
    public System.IObservable<Unit> ApproachEvent => _approachEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tagName))
        {
            _approachEvent.OnNext(Unit.Default);
            Destroy(transform.parent.gameObject);
        }
    }
}
