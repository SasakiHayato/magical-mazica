using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public interface IFusionBullet
{
    /// <summary>
    /// 破棄可能かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsDestroy(Collider2D collision, int hitCount);
    /// <summary>
    /// 破棄時
    /// </summary>
    public void Dispose();
    /// <summary>
    /// 接触時の挙動
    /// </summary>
    public void Hit(IDamagable damageble, Vector2 position);
    /// <summary>
    /// 通常時の挙動
    /// </summary>
    public void Idle();
    /// <summary>
    /// 与えるダメージ
    /// </summary>
    public int Damage { set; }
    /// <summary>
    /// 誰から撃たれた弾か
    /// </summary>
    public ObjectType ObjectType { set; }
}

/// <summary>
/// 爆発x火力<br/>
/// 真っすぐ飛んでいき、何かに接触すると爆発して消える
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
}

/// <summary>
/// 爆発x爆発<br/>
/// 放物線を描いて飛んでいき、何かに接触すると爆発して消える
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
}
/// <summary>
/// 爆発x貫通<br/>
/// 真っすぐ飛んでいき、敵に接触すると爆発する。一定回数敵に接触するか壁や地面に当たると消滅
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
}
/// <summary>
/// 爆発x毒<br/>
/// 真っすぐ飛んでいき、何かに接触したら爆発するして消える。弾か爆発に接触した敵に毒を付与する
/// </summary>
public class BlastPoison : IFusionBullet
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
        //毒付与したい
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
}
/// <summary>
/// 火力x火力<br/>
/// 真っすぐ飛んでいき、接触した敵にダメージを与える
/// </summary>
public class PowerPower : IFusionBullet
{
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position) { }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;

}
/// <summary>
/// 火力x貫通<br/>
/// 真っすぐ飛んでいき、一定数の敵に接触したら消える
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
}
public class PowerPoison : IFusionBullet
{
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        //毒付与したい
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
}
/// <summary>
/// 貫通x貫通<br/>
/// 真っすぐ飛んでいき、一定数の敵に接触したら消える
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
}
/// <summary>
/// 貫通x毒
/// </summary>
public class PenetrationPoison : IFusionBullet
{
    [SerializeField] int _destroyHitNum;
    public int Damage { private get; set; }
    public ObjectType ObjectType {  private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        //毒付与したい
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => hitCount >= _destroyHitNum;
}
public class PoisonPoison : IFusionBullet
{
    public int Damage { private get; set; }
    public ObjectType ObjectType { private get; set; }

    public void Dispose() { }

    public void Hit(IDamagable damageble, Vector2 position)
    {
        //毒付与したい
    }

    public void Idle() { }

    public bool IsDestroy(Collider2D collision, int hitCount) => true;
}