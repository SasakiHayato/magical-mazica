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
                .SetMono(_bulletPrefab) // Poolにするものの設定。 IPoolを継承しているもの
                .IsSetParent(transform) // 親の有無
                .CreateRequest(); // このメソッドを呼ぶことでPoolの作成を開始
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

        // なにかしらのEventを挟む場合
        void Click1()
        {
            Debug.Log("Click");

            ObjectPoolSampleBullet bullet = _pool.UseRequest(out System.Action action);
            // なにかしらのEvent
            bullet.SetData(Vector2.right, _bulletSpeed, transform.position);

            // Poolの使用を申請
            action.Invoke();
        }

        // 使用するだけの場合
        void Click2()
        {
            Debug.Log("Click");

            _pool.UseRequest();
        }
    }
}
