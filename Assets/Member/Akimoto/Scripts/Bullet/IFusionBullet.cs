using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface IFusionBullet
{
    /// <summary>
    /// �j���\���ǂ���
    /// </summary>
    /// <returns></returns>
    public bool IsDestroy(Collider2D collision);
    /// <summary>
    /// �j����
    /// </summary>
    public void Dispose();
    /// <summary>
    /// �ڐG���̋���
    /// </summary>
    public void Hit(IDamagable damageble, Vector2 position);
    /// <summary>
    /// �ʏ펞�̋���
    /// </summary>
    public void Idle();
    /// <summary>
    /// �^����_���[�W
    /// </summary>
    public int Damage { set; }
    /// <summary>
    /// �N���猂���ꂽ�e��
    /// </summary>
    public ObjectType ObjectType { set; }
    /// <summary>
    /// �G�Ƃ̐ڐG��
    /// </summary>
    public int HitCount { get; set; }
}

/// <summary>
/// ����x�Η�<br/>
/// �^���������ł����A�����ɐڐG����Ɣ������ď�����
/// </summary>
public class BlastPower : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }
    public int HitCount { get; set; }

    public void Idle() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        damageble.AddDamage(Damage);
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Dispose() { }

    public bool IsDestroy(Collider2D collision) => true;
}

/// <summary>
/// ����x����<br/>
/// ��������`���Ĕ��ł����A�����ɐڐG����Ɣ������ď�����
/// </summary>
public class BlastBlast : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }
    public int HitCount { get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        damageble.AddDamage(Damage);
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision) => true;
}
/// <summary>
/// ����x�ђ�<br/>
/// �^���������ł����A�G�ɐڐG����Ɣ�������B���񐔓G�ɐڐG���邩�ǂ�n�ʂɓ�����Ə���
/// </summary>
public class BlastPenetration : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    [SerializeField] int _destroyHitNum;
    /// <summary>�G�ƐڐG������</summary>
    [SerializeField] int _hitCount;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }
    public int HitCount { get => _hitCount; set => _hitCount = value; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        damageble.AddDamage(Damage);
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision)
    {
        return true;
        //if (collision.TryGetComponent(out IDamagable damageble) && damageble.ObjectType == ObjectType.Enemy)
        //{
        //    Debug.Log($"�ڐG��:{HitCount} �ݒ��:{_destroyHitNum}");
        //    if (HitCount >= _destroyHitNum)
        //    {
        //        Debug.Log("HitCount������ɒB����");
        //        return true;
        //    }
        //    else
        //    {
        //        //HitCount++;
        //        return false;
        //    }
        //}
        //else
        //{
        //    Debug.Log("Damageble�ȊO�ɐڐG����");
        //    return true;
        //}
    }
}
