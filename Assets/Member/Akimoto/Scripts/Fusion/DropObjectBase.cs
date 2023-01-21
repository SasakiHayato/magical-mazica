using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 敵からドロップするやつらの基底
/// </summary>
public class DropObjectBase : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] protected ApproachingTag _approachingTag;
    [SerializeField] protected AcquisitionDropObject _approachingDropObject;
    [SerializeField] protected float _speed;
    protected bool _isMove;

    protected void SubscribeEvent(Vector2 playerPosition)
    {
        _approachingTag.ApproachEvent
            .Where(_ => !_isMove)
            .Subscribe(_ =>
            {
                _isMove = true;
                this.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        MoveToPlayer(playerPosition);
                    })
                    .AddTo(this);
            })
            .AddTo(this);
    }

    /// <summary>
    /// プレイヤーの方に移動
    /// </summary>
    /// <param name="playerPosition"></param>
    protected void MoveToPlayer(Vector2 playerPosition)
    {
        Vector2 v = playerPosition - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }
}
