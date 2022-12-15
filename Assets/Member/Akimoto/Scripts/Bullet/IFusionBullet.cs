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
    public bool IsDestroy(Collider2D collision);
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
    /// <summary>
    /// “G‚Æ‚ÌÚG‰ñ”
    /// </summary>
    public int HitCount { get; set; }
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
/// ”š”­xŠÑ’Ê<br/>
/// ^‚Á‚·‚®”ò‚ñ‚Å‚¢‚«A“G‚ÉÚG‚·‚é‚Æ”š”­‚·‚éBˆê’è‰ñ”“G‚ÉÚG‚·‚é‚©•Ç‚â’n–Ê‚É“–‚½‚é‚ÆÁ–Å
/// </summary>
public class BlastPenetration : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    [SerializeField] int _destroyHitNum;
    /// <summary>“G‚ÆÚG‚µ‚½‰ñ”</summary>
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
        //    Debug.Log($"ÚG‰ñ”:{HitCount} İ’è‰ñ”:{_destroyHitNum}");
        //    if (HitCount >= _destroyHitNum)
        //    {
        //        Debug.Log("HitCount‚ªãŒÀ‚É’B‚µ‚½");
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
        //    Debug.Log("DamagebleˆÈŠO‚ÉÚG‚µ‚½");
        //    return true;
        //}
    }
}
