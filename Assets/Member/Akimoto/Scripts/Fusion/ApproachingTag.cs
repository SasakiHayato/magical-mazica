using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �C�ӂ̃^�O���߂��ɗ�����C�x���g�𔭍s����
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class ApproachingTag : MonoBehaviour
{
    [SerializeField] string _tagName;
    private Subject<Unit> _approachEvent = new Subject<Unit>();
    public string SetTagName { set => _tagName = value; }
    /// <summary>�Ώۂ��ڋ߂��Ă����ۂ̃C�x���g</summary>
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
