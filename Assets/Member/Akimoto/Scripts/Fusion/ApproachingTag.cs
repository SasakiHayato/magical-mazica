using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 任意のタグが近くに来たらイベントを発行する
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class ApproachingTag : MonoBehaviour
{
    [SerializeField] string _tagName;
    private Subject<Unit> _approachEvent = new Subject<Unit>();
    public string SetTagName { set => _tagName = value; }
    /// <summary>対象が接近してきた際のイベント</summary>
    public System.IObservable<Unit> ApproachEvent => _approachEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tagName))
        {
            SoundManager.PlayRequest(SoundSystem.SoundType.SEPlayer, "GetItem");
            _approachEvent.OnNext(Unit.Default);
            //Destroy(transform.parent.gameObject);
        }
    }
}
