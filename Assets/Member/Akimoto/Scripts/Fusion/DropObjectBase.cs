using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �G����h���b�v������̊��
/// </summary>
public class DropObjectBase : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] protected ApproachingTag _approachingTag;
    [SerializeField] protected AcquisitionDropObject _approachingDropObject;
    [SerializeField] protected float _speed;
    protected bool _isMove;

    /// <summary>
    /// �ڐG������󂯎��
    /// </summary>
    /// <param name="player"></param>
    protected void SubscribeApproachingEvent(GameObject player)
    {
        _approachingTag.ApproachEvent
            .Where(_ => !_isMove)
            .Subscribe(_ =>
            {
                _isMove = true;
                this.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        if (player != null)
                        {
                            MoveToPlayer(player);
                        }
                        
                    })
                    .AddTo(this);
            })
            .AddTo(this);
    }

    /// <summary>
    /// �v���C���[�̕��Ɉړ�
    /// </summary>
    /// <param name="playerPosition"></param>
    protected void MoveToPlayer(GameObject targetObject)
    {
        Vector2 v = (Vector2)targetObject.transform.position - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }
}
