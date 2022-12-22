using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] int _power;
    
    Collider2D _collider;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _collider.enabled = false;
    }

    public void SetColliderActive(bool isActive)
    {
        if (_collider == null)
        {
            Setup();
        }
        _collider.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out IDamagable damageble))
        {
            damageble.AddDamage(_power);
            EffectStocker.Instance.LoadEffect("Hit", transform.position);
        }
    }
}
