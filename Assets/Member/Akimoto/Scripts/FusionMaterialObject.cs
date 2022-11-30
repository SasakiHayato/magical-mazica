using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ObjectPool;

/// <summary>
/// �t�B�[���h��Ƀh���b�v����f�ރI�u�W�F�N�g
/// </summary>
public class FusionMaterialObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ApproachingTag _approachingTag;
    [SerializeField] float _speed;
    private RawMaterialDatabase _materialData;
    private bool _isMove = false;
    /// <summary>�f�ރf�[�^</summary>
    public RawMaterialDatabase MaterialID => _materialData;

    /// <summary>
    /// �f�ރI�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="original">������(Prefab)</param>
    /// <param name="createPosition">�������W</param>
    /// <param name="data">��������f�ރf�[�^</param>
    /// <param name="player">���݂̃v���C���[</param>
    /// <returns></returns>
    public static FusionMaterialObject Init(FusionMaterialObject original, Vector2 createPosition, RawMaterialDatabase data, Player player)
    {
        FusionMaterialObject ret = Instantiate(original, createPosition, Quaternion.identity);
        Debug.Log(data);
        Debug.Log(player);
        ret.Setup(data, player);
        return ret;
    }

    private void Setup(RawMaterialDatabase data, Player player)
    {
        _spriteRenderer.sprite = data.Sprite;
        _spriteRenderer.color = data.SpriteColor;

        //�v���C���[���ڋ߂��Ă��瓮����
        _approachingTag.ApproachEvent
            .Where(_ => !_isMove)
            .Subscribe(_ =>
            {
                _isMove = true;
                this.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        MoveToPlayer(player.transform.position);
                    })
                    .AddTo(this);
            })
            .AddTo(this);
    }

    /// <summary>
    /// �v���C���[�̕��Ɉړ�
    /// </summary>
    /// <param name="playerPosition"></param>
    private void MoveToPlayer(Vector2 playerPosition)
    {
        Vector2 v = playerPosition - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }
}
