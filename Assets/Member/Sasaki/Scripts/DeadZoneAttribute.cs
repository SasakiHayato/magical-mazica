using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneAttribute : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out IDamagable damagable))
        {
            EffectStocker.Instance.LoadEffect("Dead1", collision.transform.position);
            EffectStocker.Instance.LoadEffect("Dead2", collision.transform.position);
            EffectStocker.Instance.LoadEffect("Dead3", collision.transform.position);

            damagable.AddDamage(int.MaxValue);
        }
    }
}
