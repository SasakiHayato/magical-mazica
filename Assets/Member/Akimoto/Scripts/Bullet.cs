using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
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
    /// <summary>�ڐG��</summary>
    private int _hitCount;
    /// <summary>�ڐG�Ώۂɕt�^�����Ԉُ�</summary>
    private StatusEffectBase _statusEffect;
    public Vector2 Velocity { set => _rb.velocity = value; }
    public ObjectType ObjectType { get; set; } = ObjectType.Obstacle;
    public BulletType UseType => _useType;
    public FusionDatabase Database { private get; set; }
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
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (Database != null && Database.FusionBullet != null)
                {
                    Database.FusionBullet.Idle();
                }
            })
            .AddTo(this);
    }

    /// <summary>
    /// �摜��p�����[�^�[�̐ݒ�
    /// </summary>
    public void Setup(Sprite sprite, BulletType useType, int damage, StatusEffectBase statusEffect)
    {
        _statusEffect = statusEffect;
        Setup(sprite, useType, damage);
    }

    /// <summary>
    /// �Z������Bullet�̃C���X�^���X�𐶐����ĕԂ�
    /// </summary>
    public static Bullet Init(Bullet original, FusionDatabase database, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(database.Sprite, database.UseType, damage);
        ret.Database = database.Copy();
        ret.Database.FusionBullet.Damage = damage;
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
                if (Database != null && Database.FusionBullet != null)
                {
                    Database.FusionBullet.Hit(damagable, transform.position);
                    _hitCount++;

                    //��Ԉُ�t�^�\�Ȃ�Ώۂɕt�^����
                    var eft = Database.FusionBullet.StatusEffect();
                    if (collision.TryGetComponent(out EnemyBase enemyBase) && eft != null)
                    {
                        enemyBase.SetStatusEffect = eft;
                        eft.EndEvent
                            .Subscribe(_ => enemyBase.StatusEffectRemove(eft))
                            .AddTo(enemyBase);
                        eft.Effect(damagable.AddDamage, enemyBase);
                    }
                }

                damagable.AddDamage(_damage);

                if (collision.TryGetComponent(out IDamageForceble forceble))
                {
                    // ��
                    forceble.OnFoece(Vector2.zero);
                }

                if (Database != null && Database.FusionBullet != null)
                {
                    if (Database.FusionBullet.IsDestroy(collision, _hitCount))
                    {
                        Database.FusionBullet.Dispose();
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
