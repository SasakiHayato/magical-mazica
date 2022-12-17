using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface IFusionBullet
{
    /// <summary>
    /// ”jŠü‰Â”\‚©‚Ç‚¤‚©
    /// </summary>
    /// <returns></returns>
    public bool IsDestroy(Collider2D collision, int hitCount);
    /// <summary>
    /// ”jŠü
    /// </summary>
    public void Dispose();
    /// <summary>
    /// ÚG‚Ì‹““®
    /// </summary>
    public void Hit(IDamagable damageble, Vector2 position);
    /// <summary>
    /// ’Êí‚Ì‹““®
    /// </summary>
    public void Idle();
    /// <summary>
    /// •t—^‚·‚éó‘ÔˆÙí
    /// </summary>
    public StatusEffectBase StatusEffect();
    /// <summary>
    /// —^‚¦‚éƒ_ƒ[ƒW
    /// </summary>
    public int Damage { set; }
    /// <summary>
    /// ’N‚©‚çŒ‚‚½‚ê‚½’e‚©
    /// </summary>
    public ObjectType ObjectType { set; }
}

/// <summary>
/// ”š”­x‰Î—Í<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«A‰½‚©‚ÉÚG‚·‚é‚Æ”š”­‚µ‚ÄÁ‚¦‚é
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
/// ”š”­x”š”­<br/>
/// •ú•¨ü‚ğ•`‚¢‚Ä”ò‚ñ‚Å‚¢‚«A‰½‚©‚ÉÚG‚·‚é‚Æ”š”­‚µ‚ÄÁ‚¦‚é
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
/// ”š”­xŠÑ’Ê<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«A“G‚ÉÚG‚·‚é‚Æ”š”­‚·‚éBˆê’è‰ñ”“G‚ÉÚG‚·‚é‚©•Ç‚â’n–Ê‚É“–‚½‚é‚ÆÁ–Å
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
/// ”š”­x“Å<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«A‰½‚©‚ÉÚG‚µ‚½‚ç”š”­‚·‚é‚µ‚ÄÁ‚¦‚éB’e‚©”š”­‚ÉÚG‚µ‚½“G‚É“Å‚ğ•t—^‚·‚é
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
/// ‰Î—Íx‰Î—Í<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«AÚG‚µ‚½“G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
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
/// ‰Î—ÍxŠÑ’Ê<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«Aˆê’è”‚Ì“G‚ÉÚG‚µ‚½‚çÁ‚¦‚é
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
/// ŠÑ’ÊxŠÑ’Ê<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«Aˆê’è”‚Ì“G‚ÉÚG‚µ‚½‚çÁ‚¦‚é
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
/// ŠÑ’Êx“Å
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