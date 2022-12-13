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
    [SerializeField] float _throwBulletHeightOffset;
    /// <summary>アイテム名</summary>
    private ReactiveProperty<string> _name = new ReactiveProperty<string>();
    /// <summary>アイコン</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>ダメージ</summary>
    private int _damage;
    /// <summary>現在保持している融合データベース</summary>
    private FusionDatabase _database;
    public System.IObservable<string> ChangeNameObservable => _name;
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

        _name.Value = data.Name;
        _icon.Value = data.Icon;
        _database = data;
    }

    /// <summary>
    /// 融合したものを使用して攻撃する<br/>
    /// 攻撃時にプレイヤークラスからこの関数を呼ぶこと
    /// </summary>
    public void Attack(Vector2 directions)
    {
        //融合前は攻撃不可
        if (_database == null)
        {
            Debug.Log("先に融合してください");
            return;
        }
        //生成
        Bullet blt = Bullet.Init(_bulletPrefab, _database, _damage);
        blt.transform.position = transform.position;
        blt.Velocity = BulletDirectionOffset(directions, blt) * _database.BulletSpeed;
        Dispose();
    }

    private Vector2 BulletDirectionOffset(Vector2 direction, Bullet bullet)
    {
        Vector2 ret = new Vector2(direction.x, direction.y);
        switch (bullet.UseType)
        {
            case BulletType.Throw:
                ret = new Vector2(direction.x, direction.y + _throwBulletHeightOffset);
                break;
            case BulletType.Strike:
                break;
        }
        return ret;
    }

    /// <summary>
    /// データの破棄
    /// </summary>
    private void Dispose()
    {
        _database = null;
        _name.Value = "";
        _icon.Value = null;
    }
}
