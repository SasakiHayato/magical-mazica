using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Blast : MonoBehaviour
{
    [SerializeField] GameObject _particleEffect;
    /// <summary>égópé“ÇÃObjectType</summary>
    private ObjectType _objectType;
    private int _damage;
    private StatusEffectBase _statusEffect;

    public static Blast Init(Blast original, Vector2 position, float range, float duration, int damage, ObjectType objectType)
    {
        Blast ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(range, duration, damage, objectType);
        return ret;
    }

    public static Blast Init(Blast original, Vector2 position, float range, float duration, int damage, StatusEffectBase statusEffect, ObjectType objectType)
    {
        Blast ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(range, duration, damage, objectType);
        return ret;
    }

    public void Setup(float range, float duration, int damage, StatusEffectBase statusEffect, ObjectType objectType)
    {
        _statusEffect = statusEffect;
        Setup(range, duration, damage, objectType);
    }

    public void Setup(float range, float duration, int damage, ObjectType objectType)
    {
        transform.localScale = new Vector2(range, range);
        _damage = damage;
        _objectType = objectType;
        DestroyCount(duration);
        if (_particleEffect)
            Instantiate(_particleEffect, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// éwíËïbêîë“Ç¡ÇƒÇ©ÇÁè¡Ç¶ÇÈ
    /// </summary>
    /// <param name="duration"></param>
    private async void DestroyCount(float duration)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(duration));
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damageble))
        {
            if (damageble.ObjectType != _objectType)
            {
                damageble.AddDamage(_damage);

                if (collision.TryGetComponent(out EnemyBase enemyBase) && _statusEffect != null)
                {
                    enemyBase.SetStatusEffect = _statusEffect;
                }

                if (collision.TryGetComponent(out IDamageForceble forceble))
                {
                    // âº
                    forceble.OnFoece(Vector2.zero);
                }
            }
        }
    }
}