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
    [SerializeField] float _speed;
    private RawMaterialDatabase _materialData;
    /// <summary>�f��</summary>
    public RawMaterialDatabase MaterialID => _materialData;

    /// <summary>
    /// �f�ރI�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="original">������(Prefab)</param>
    /// <param name="createPosition">�������W</param>
    /// <param name="data">��������f�ރf�[�^</param>
    /// <param name="player"></param>
    /// <returns></returns>
    public static FusionMaterialObject Init(FusionMaterialObject original, Vector2 createPosition, RawMaterialDatabase data, Player player)
    {
        FusionMaterialObject ret = Instantiate(original, createPosition, Quaternion.identity);
        ret.Setup(data, player);
        return ret;
    }

    private void Setup(RawMaterialDatabase data, Player player)
    {
        _spriteRenderer.sprite = data.Sprite;
        //�f�[�^�������Ă���Update
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log(player.transform.position);
                MoveToPlayer(player.transform.position);
            })
            .AddTo(this);
    }

    /// <summary>
    /// �v���C���[�̕��ɓ�����
    /// </summary>
    /// <param name="playerPosition"></param>
    private void MoveToPlayer(Vector2 playerPosition)
    {
        Vector2 v = playerPosition - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }
}
