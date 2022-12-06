using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] Player _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EffectStocker.Instance.LoadFieldEffect(FieldEffect.EffectType.CmShake);
            GameObject go = collision.transform.gameObject;
            go.GetComponent<IDamagable>().AddDamage(_player.Damage);
        }
    }
}
