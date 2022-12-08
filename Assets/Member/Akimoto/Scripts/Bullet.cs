using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �e�̐���
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    /// <summary>�摜</summary>
    private Sprite _sprite;
    /// <summary>��ѕ�</summary>
    private BulletType _useType;
    /// <summary>�_���[�W</summary>
    private int _damage;
    public Vector2 Velocity { set => _rb.velocity = value; }
    public ObjectType ObjectType { get; set; } = ObjectType.Obstacle;

    /// <summary>
    /// �摜��p�����[�^�[�̐ݒ�
    /// </summary>
    public void Setup(Sprite sprite, BulletType useType, int damage)
    {
        _sprite = sprite;
        _useType = useType;
        _damage = damage;
        switch (_useType)
        {
            case BulletType.Throw:
                _rb.gravityScale = 1;
                break;
            case BulletType.Strike:
                _rb.gravityScale = 0;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �Z������Bullet�̃C���X�^���X�𐶐����ĕԂ�
    /// </summary>
    public static Bullet Init(Bullet original, FusionDatabase database, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(database.Sprite, database.UseType, damage);
        return ret;
    }

    /// <summary>
    /// Bullet�̃C���X�^���X�𐶐����ĕԂ�
    /// </summary>
    public static Bullet Init(Bullet original, Sprite sprite, BulletType useType, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(sprite, useType, damage);
        return ret;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
        {
            if (damagable.ObjectType != ObjectType)
            {
                damagable.AddDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
