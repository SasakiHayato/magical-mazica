using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 融合アイテムの管理クラス<br/>プレイヤースクリプトと一緒に付けて使う
/// </summary>
public class FusionItem : MonoBehaviour
{
    [SerializeField] FusionData _fusionData;
    [SerializeField] Bullet _bulletPrefab;
    /// <summary>アイテム名</summary>
    private string _name;
    /// <summary>アイコン</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>ダメージ</summary>
    private int _damage;
    /// <summary>現在保持している融合データベース</summary>
    private FusionDatabase _database;
    /// <summary>アイコン画像の更新を通知する</summary>
    public System.IObservable<Sprite> ChangeIconObservable => _icon;

    public void Setup()
    {
        
    }

    /// <summary>
    /// 融合開始<br/>渡された素材から融合後のデータを抽出する
    /// </summary>
    public void Fusion(RawMaterialID materialID1, RawMaterialID materialID2)
    {
        var data = _fusionData.GetFusionData(materialID1, materialID2, ref _damage);

        _name = data.Name;
        _icon.Value = data.Icon;
        _database = data;
    }

    /// <summary>
    /// 融合したものを使用して攻撃する<br/>
    /// 攻撃時にプレイヤークラスからこの関数を呼ぶこと
    /// </summary>
    public void Attack(Vector2 directions)
    {
        Bullet blt = Bullet.Init(_bulletPrefab, _database, _damage);
        blt.transform.position = transform.position;
        blt.Velocity = directions;
    }
}
