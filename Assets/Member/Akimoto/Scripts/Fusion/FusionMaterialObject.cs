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
public class FusionMaterialObject : DropObjectBase
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private RawMaterialDatabase _materialData;

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
        ret.Setup(data, player);
        Debug.Log($"ドロップした素材{data.Name}");
        return ret;
    }

    private void Setup(RawMaterialDatabase data, Player player)
    {
        _spriteRenderer.sprite = data.Sprite;
        _spriteRenderer.color = data.SpriteColor;
        _materialData = data;

        SubscribeApproachingEvent(player.gameObject);
        _approachingDropObject.SetAction = () =>
        {
            player.Storage.AddMaterial(_materialData.ID, 1);
            Destroy(gameObject);
        };
    }
}
