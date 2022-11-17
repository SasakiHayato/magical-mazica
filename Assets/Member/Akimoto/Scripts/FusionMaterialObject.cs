using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// フィールド上にドロップする素材オブジェクト
/// </summary>
public class FusionMaterialObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _speed;
    private RawMaterialDatabase _materialData;

    public void Setup(RawMaterialDatabase data, Player player)
    {
        _spriteRenderer.sprite = data.Sprite;

        //データが入ってからUpdate動かす
        this.UpdateAsObservable()
            .Subscribe(_ => MoveToPlayer(player.transform.position))
            .AddTo(this);
    }

    private void MoveToPlayer(Vector2 playerPosition)
    {
        Vector2 v = playerPosition - (Vector2)transform.position;
        v.Normalize();
        transform.position = v * _speed;
    }
}
