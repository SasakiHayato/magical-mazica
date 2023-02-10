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
    [SerializeField] protected float _initMoveDuraion;
    protected bool _isMove;
    protected bool _initMoveConpleate;
    private bool _isDestroy;
    private Sequence _sequence;

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
                    .Subscribe(async _ =>
                    {
                        if (!_initMoveConpleate)
                        {
                            await UniTask.WaitUntil(() => _initMoveConpleate);
                        }
                        _approachingDropObject.ActionFlag = true;
                        MoveToPlayer(player);
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
        if (_isDestroy)
            return;
        Vector2 v = (Vector2)targetObject.transform.position - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }

    /// <summary>
    /// ��������̉��o�p�̈ړ�
    /// </summary>
    /// <param name="endPisition">�ڕW���W</param>
    /// <param name="duraion">�ړ�����</param>
    /// <param name="onConpleate">�I����̐U�镑��</param>
    protected void InitMove(Vector2 endPisition, System.Action onConpleate = null)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMove(endPisition, _initMoveDuraion))
            .OnComplete(() =>
            {
                if (onConpleate != null)
                {
                    onConpleate();
                }
                _initMoveConpleate = true;
            });
    }

    private void OnDestroy()
    {
        _isDestroy = true;
    }
}
