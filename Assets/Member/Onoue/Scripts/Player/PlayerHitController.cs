using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] Player _player;

    readonly string EnemyTag = "Enemy";
    readonly float ForcePower = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(EnemyTag)) return;

        SetEffect();
        Process(collision.gameObject.transform);
    }

    void Process(Transform target)
    {
        if (target.TryGetComponent(out IDamagable damagable))
        {
            damagable.AddDamage(_player.Damage);
        }

        if (target.TryGetComponent(out IDamageForceble forceble))
        {
            Vector2 forceDirection = target.position - _player.transform.position;
            forceDirection.y = 1;
            forceble.OnFoece(forceDirection.normalized * ForcePower);
        }
    }

    void SetEffect()
    {
        EffectStocker.Instance.LoadEffect("Hit", transform.position);
        EffectStocker.Instance.LoadFieldEffect(FieldEffect.EffectType.HitStop, CameraOperator.ShakePower);
        EffectStocker.Instance.LoadFieldEffect(FieldEffect.EffectType.CmShake, FieldManager.DefaultHitStopTime);
    }
}
