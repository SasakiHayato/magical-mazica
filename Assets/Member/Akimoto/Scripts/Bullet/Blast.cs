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

    public static Blast Init(Blast original, Vector2 position, float range, float duration, int damage, ObjectType objectType)
    {
        Blast ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(range, duration, damage, objectType);
        return ret;
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

    private async void DestroyCount(float duration)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(duration));
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable obj))
        {
            if (obj.ObjectType != _objectType)
            {
                obj.AddDamage(_damage);

                if (TryGetComponent(out IDamageForceble forceble))
                {
                    // âº
                    forceble.OnFoece(Vector2.zero);
                }
            }
        }
    }
}