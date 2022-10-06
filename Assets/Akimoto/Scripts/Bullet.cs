using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    /// <summary>画像</summary>
    private Sprite _sprite;
    /// <summary>飛び方</summary>
    private UseType _useType;
    /// <summary>ダメージ</summary>
    private int _damage;

    /// <summary>
    /// 画像やパラメーターの設定
    /// </summary>
    public void Setup(Sprite sprite, UseType useType, int damage)
    {
        _sprite = sprite;
        _useType = useType;
        _damage = damage;
    }

    /// <summary>
    /// Bulletのインスタンスを生成して返す
    /// </summary>
    public static Bullet Init(Bullet original, Sprite sprite, UseType useType, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(sprite, useType, damage);
        return ret;
    }
}
