using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 融合アイテムの管理クラス
/// </summary>
public class FusionItem : MonoBehaviour
{
    [SerializeField] FusionData _fusionData;
    [SerializeField] Bullet _bulletPrefab;
    /// <summary>アイテム名</summary>
    private string _name;
    /// <summary>アイコン</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>投げた時の画像</summary>
    private Sprite _sprite;
    /// <summary>ダメージ</summary>
    private int _damage;
    /// <summary>使用した際の挙動</summary>
    private UseType _useType;
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
        _sprite = data.Sprite;
        _useType = data.UseType;
    }

    /// <summary>
    /// 融合したものを使用して攻撃する
    /// </summary>
    public void Attack()
    {
        Bullet blt = Bullet.Init(_bulletPrefab, _sprite, _useType, _damage);

    }

    /// <summary>
    /// データが正しく入っているかを確かめるテスト用関数
    /// </summary>
    public void CheckData()
    {
        Debug.Log($"Name;{_name} UseType:{_useType} Damage:{_damage}");
    }
}
