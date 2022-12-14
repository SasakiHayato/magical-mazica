using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ObjectPool;

/// <summary>
/// フィールド上にドロップする素材オブジェクト
/// </summary>
public class FusionMaterialObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ApproachingTag _approachingTag;
    [SerializeField] float _speed;
    private RawMaterialDatabase _materialData;
    private bool _isMove = false;
    /// <summary>素材データ</summary>
    public RawMaterialDatabase MaterialID => _materialData;

    /// <summary>
    /// 素材オブジェクトの生成
    /// </summary>
    /// <param name="original">生成元(Prefab)</param>
    /// <param name="createPosition">生成座標</param>
    /// <param name="data">生成する素材データ</param>
    /// <param name="player">現在のプレイヤー</param>
    /// <returns></returns>
    public static FusionMaterialObject Init(FusionMaterialObject original, Vector2 createPosition, RawMaterialDatabase data, Player player = null)
    {
        if (data == null) return null;

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

        //プレイヤーが接近してから動かす
        _approachingTag.ApproachEvent
            .Where(_ => player != null)
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
    /// プレイヤーの方に移動
    /// </summary>
    /// <param name="playerPosition"></param>
    private void MoveToPlayer(Vector2 playerPosition)
    {
        Vector2 v = playerPosition - (Vector2)transform.position;
        v.Normalize();
        _rb.velocity = v * _speed;
    }
}
