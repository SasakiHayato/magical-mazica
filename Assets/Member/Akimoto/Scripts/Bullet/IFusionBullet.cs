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
    public bool IsDestroy(Collider2D collision, int hitCount);
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
    /// �t�^�����Ԉُ�
    /// </summary>
    public StatusEffectBase StatusEffect();
    /// <summary>
    /// �^����_���[�W
    /// </summary>
    public int Damage { set; }
    /// <summary>
    /// �N���猂���ꂽ�e��
    /// </summary>
    public ObjectType ObjectType { set; }
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

    public void Idle() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Dispose() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;

    public StatusEffectBase StatusEffect() => null;
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

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
    public StatusEffectBase StatusEffect() => null;
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
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount)
    {
        if (collision.TryGetComponent(out IDamagable damageble) && damageble.ObjectType == ObjectType.Enemy)
        {
            return hitCount >= _destroyHitNum;
        }
        else
        {
            return true;
        }
    }
    public StatusEffectBase StatusEffect() => null;
}
/// <summary>
/// ����x��<br/>
/// �^���������ł����A�����ɐڐG�����甚�����邵�ď�����B�e�������ɐڐG�����G�ɓł�t�^����
/// </summary>
public class BlastPoison : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    [SerializeField] int _slipDamage;
    [SerializeField] int _duration;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }
    //public StatusEffectBase StatusEffect
    //{
    //    get
    //    {
    //        Poison ret = (Poison)SelectEffect.AssignmentEffect(StatusEffectID.Posion, _duration);
    //        ret.Damage = _slipDamage;
    //        return ret;
    //    }
    //}

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, StatusEffect(), ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
    public StatusEffectBase StatusEffect()
    {
        Poison ret = (Poison)SelectEffect.AssignmentEffect(StatusEffectID.Posion, _duration);
        ret.Damage = _slipDamage;
        return ret;
    }
}
/// <summary>
/// �Η�x�Η�<br/>
/// �^���������ł����A�ڐG�����G�Ƀ_���[�W��^����
/// </summary>
public class PowerPower : IFusionBullet
{
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
    public StatusEffectBase StatusEffect() => null;

}
/// <summary>
/// �Η�x�ђ�<br/>
/// �^���������ł����A��萔�̓G�ɐڐG�����������
/// </summary>
public class PowerPenetration : IFusionBullet
{
    [SerializeField] int _destroyHitNum;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => hitCount >= _destroyHitNum;
    public StatusEffectBase StatusEffect() => null;
}
public class PowerPoison : IFusionBullet
{
    [SerializeField] int _slipDamage;
    [SerializeField] int _duration;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;

    public StatusEffectBase StatusEffect()
    {
        Poison ret = (Poison)SelectEffect.AssignmentEffect(StatusEffectID.Posion, _duration);
        ret.Damage = _slipDamage;
        return ret;
    }
}
/// <summary>
/// �ђ�x�ђ�<br/>
/// �^���������ł����A��萔�̓G�ɐڐG�����������
/// </summary>
public class PenetrationPenetration : IFusionBullet
{
    [SerializeField] int _destroyHitNum;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        damageble.AddDamage(Damage);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => hitCount >= _destroyHitNum;

    public StatusEffectBase StatusEffect() => null;
}
/// <summary>
/// �ђ�x��
/// </summary>
public class PenetrationPoison : IFusionBullet
{
    [SerializeField] int _destroyHitNum;
    [SerializeField] int _duration;
    [SerializeField] int _slipDamage;
    public int Damage { private get; set; }
    public ObjectType ObjectType {  private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => hitCount >= _destroyHitNum;
    public StatusEffectBase StatusEffect()
    {
        Poison ret = (Poison)SelectEffect.AssignmentEffect(StatusEffectID.Posion, _duration);
        ret.Damage = _slipDamage;
        return ret;
    }
}
public class PoisonPoison : IFusionBullet
{
    [SerializeField] int _slipDamage;
    [SerializeField] int _duration;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
    public StatusEffectBase StatusEffect()
    {
        Poison ret = (Poison)SelectEffect.AssignmentEffect(StatusEffectID.Posion, _duration);
        ret.Damage = _slipDamage;
        return ret;
    }
}