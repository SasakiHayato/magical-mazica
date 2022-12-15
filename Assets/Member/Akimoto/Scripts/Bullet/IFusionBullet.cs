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
        damageble.AddDamage(Damage);
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Dispose() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
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
        damageble.AddDamage(Damage);
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
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
        damageble.AddDamage(Damage);
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
}
