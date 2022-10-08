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
    /// <summary>�摜</summary>
    private Sprite _sprite;
    /// <summary>��ѕ�</summary>
    private UseType _useType;
    /// <summary>�_���[�W</summary>
    private int _damage;
    public Rigidbody2D Rigidbody => _rb;

    /// <summary>
    /// �摜��p�����[�^�[�̐ݒ�
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
    /// Bullet�̃C���X�^���X�𐶐����ĕԂ�
    /// </summary>
    public static Bullet Init(Bullet original, FusionDatabase database, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(database.Sprite, database.UseType, damage);
        return ret;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Ƃ肠����
        if (!collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
