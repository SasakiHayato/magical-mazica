using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    /// <summary>画像</summary>
    private Sprite _sprite;
    /// <summary>飛び方</summary>
    private UseType _useType;
    /// <summary>ダメージ</summary>
    private int _damage;
    public Rigidbody2D Rigidbody => _rb;

    /// <summary>
    /// 画像やパラメーターの設定
    /// </summary>
    public void Setup(Sprite sprite, UseType useType, int damage)
    {
        _sprite = sprite;
        _useType = useType;
        _damage = damage;
        switch (_useType)
        {
            case UseType.Throw:
                _rb.gravityScale = 1;
                break;
            case UseType.Strike:
                _rb.gravityScale = 0;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Bulletのインスタンスを生成して返す
    /// </summary>
    public static Bullet Init(Bullet original, FusionDatabase database, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(database.Sprite, database.UseType, damage);
        return ret;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //とりあえず
        if (!collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
