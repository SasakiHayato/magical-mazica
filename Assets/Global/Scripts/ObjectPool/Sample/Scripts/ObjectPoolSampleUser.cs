using UnityEngine;

namespace ObjectPool.Sample
{
    public class ObjectPoolSampleUser : MonoBehaviour
    {
        [SerializeField] Transform _muzzle;
        [SerializeField] ObjectPoolSampleBullet _bulletPrefab;
        [SerializeField] float _bulletSpeed;

        Pool<ObjectPoolSampleBullet> _pool = new Pool<ObjectPoolSampleBullet>();

        void Start()
        {
            _pool
                .SetMono(_bulletPrefab) // Pool�ɂ�����̂̐ݒ�B IPool���p�����Ă������
                .IsSetParent(transform) // �e�̗L��
                .CreateRequest(); // ���̃��\�b�h���ĂԂ��Ƃ�Pool�̍쐬���J�n
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click1();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Click2();
            }
        }

        // �Ȃɂ������Event�����ޏꍇ
        void Click1()
        {
            Debug.Log("Click");

            ObjectPoolSampleBullet bullet = _pool.UseRequest(out System.Action action);
            // �Ȃɂ������Event
            bullet.SetData(Vector2.right, _bulletSpeed, transform.position);

            // Pool�̎g�p��\��
            action.Invoke();
        }

        // �g�p���邾���̏ꍇ
        void Click2()
        {
            Debug.Log("Click");

            _pool.UseRequest();
        }
    }
}
