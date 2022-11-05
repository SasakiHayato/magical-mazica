using UnityEngine;

namespace EnemyAISystem
{
    public class EnemyAttackCollider : MonoBehaviour
    {
        [SerializeField] int _power;

        Collider2D _collider;

        void Start()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }

        public void SetColliderActive(bool isActive)
        {
            _collider.enabled = isActive;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damageble))
            {
                damageble.AddDamage(_power);
            }
        }
    }
}
