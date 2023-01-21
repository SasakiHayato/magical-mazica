using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �C�ӂ̃^�O���߂��ɗ�����C�x���g�𔭍s����
/// </summary>
public class ApproachingTag : MonoBehaviour
{
    [SerializeField] string _tagName;
    private Subject<Unit> _approachEvent = new Subject<Unit>();
    /// <summary>�Ώۂ��ڋ߂��Ă����ۂ̃C�x���g</summary>
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
