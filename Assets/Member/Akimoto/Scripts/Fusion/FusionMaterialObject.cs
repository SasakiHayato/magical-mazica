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
    private Sequence _sequence;

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
        return ret;
    }

    /// <summary>
    /// 素材オブジェクトを生成<br/>
    /// ある程度位置をばらけさせる
    /// </summary>
    /// <param name="original">生成元(Prefab)</param>
    /// <param name="createPosition">中心とする生成座標</param>
    /// <param name="randomRange">生成範囲</param>
    /// <param name="data">生成する素材データ</param>
    /// <param name="player">現在のプレイヤー</param>
    /// <returns></returns>
    public static FusionMaterialObject Init(FusionMaterialObject original, Vector2 createPosition, float randomRange, RawMaterialDatabase data, Player player = null)
    {
        if (data == null) return null;

        FusionMaterialObject ret = Instantiate(original, createPosition, Quaternion.identity);
        float xPos = Random.Range(createPosition.x - randomRange, createPosition.x + randomRange);
        float yPos = Random.Range(createPosition.y - randomRange, createPosition.y + randomRange);
        //ret.transform.position = createPosition;
        ret.transform.position = new Vector2(xPos, yPos);
        ret.Setup(data, player);
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
