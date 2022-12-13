using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface IFusionBullet
{
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
    public int Damage { set; }
    public ObjectType ObjectType { set; }
}

/// <summary>
/// ”š”­x‰Î—Í
/// </summary>
public class BlastPower : IFusionBullet
{
    [SerializeField] Blast _blastPrefab;
    [SerializeField] float _blastRange;
    [SerializeField] float _blastDuraion;
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Idle()
    {

    }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        Blast.Init(_blastPrefab, position, _blastRange, _blastDuraion, Damage, ObjectType);
    }

    public void Dispose()
    {

    }
}

/// <summary>
/// ”š”­x”š”­
/// </summary>
public class BlastBlast : IFusionBullet
{
    public int Damage { set => throw new System.NotImplementedException(); }
    public ObjectType ObjectType { set => throw new System.NotImplementedException(); }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }
}