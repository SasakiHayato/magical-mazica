using UnityEngine;
using ObjectPool.Event;

namespace ObjectPool.Sample
{
    // Note. IPoolEvent�͕K�v�ɉ����Čp������B
    public class ObjectPoolSampleBullet : MonoBehaviour, IPool, IPoolOnEnableEvent, IPoolEvent, IPoolDispose
    {
        [SerializeField] float _activeTime = 1;

        float _timer = 0;
        float _speed = 5;
        bool _isCollision = false;
        Vector2 _direction = Vector2.right;

        Rigidbody2D _rb;

        // �C�ӂ̃^�C�~���O��Pool��j���������ꍇ��True��Ԃ�
        bool IPoolEvent.IsDone => _isCollision;

        void IPool.Setup(Transform parent)
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;

            GetComponent<Collider2D>().isTrigger = true;

            Debug.Log("Setup");
        }

        // Pool�g�p����1�x�����Ă΂��BUnityEngine.OnEnable();�̑�p
        void IPoolOnEnableEvent.OnEnableEvent()
        {
            _timer = 0;
            _isCollision = false;

            Debug.Log("OnEnable");
        }

        public void SetData(Vector2 direction, float speed, Vector3 setPosition)
        {
            _direction = direction;
            _speed = speed;

            transform.position = setPosition;

            Debug.Log("SetData");
        }

        bool IPool.Execute()
        {
            Debug.Log("Execute");

            _rb.velocity = _direction * _speed;

            _timer += Time.deltaTime;
            return _timer > _activeTime;
        }

        // Pool���j�����ꂽ���Ɉ�x�����Ă΂��BUnityEngine.OnDestroy();�̑�p
        void IPoolDispose.Dispose()
        {
            Debug.Log("Dispose");

            _direction = Vector2.right;
            _speed = 5;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("OnCollision");
            _isCollision = true;
        }
    }
}
