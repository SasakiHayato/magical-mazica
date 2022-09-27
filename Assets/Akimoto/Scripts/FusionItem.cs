using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 融合アイテムのインスタンス用クラス
/// </summary>
public class FusionItem : MonoBehaviour
{
    /// <summary>アイテム名</summary>
    private string _name;
    /// <summary>アイコン</summary>
    private Sprite _icon;
    /// <summary>投げた時の画像</summary>
    private Sprite _sprite;
    /// <summary>ダメージ</summary>
    private int _damage;
    /// <summary>使用した際の挙動</summary>
    private UseType _useType;

    public void Setup(FusionDatabase fusionDatabase, int damage)
    {
        _name = fusionDatabase.Name;
        _icon = fusionDatabase.Icon;
        _sprite = fusionDatabase.Sprite;
        _useType = fusionDatabase.UseType;
        _damage = damage;
    }

    /// <summary>
    /// データが正しく入っているかを確かめるテスト用関数
    /// </summary>
    public void CheckData()
    {
        Debug.Log($"Name;{_name} UseType:{_useType} Damage:{_damage}");
    }
}
