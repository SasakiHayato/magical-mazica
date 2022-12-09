using UnityEngine;

public class ShotBullet : IBossAttackTask
{
    [SerializeField] float _speed = 1;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _muzzle;

    Sprite _source;

    void IBossAttackTask.Setup(Transform user, EnemyAttackCollider attackCollider)
    {
        _source = _bulletPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    void IBossAttackTask.Execute()
    {
        Vector2 direction = GameController.Instance.Player.position - _muzzle.position;
        Bullet bullet = Bullet.Init(_bulletPrefab, _source, BulletType.Strike, 1);
        bullet.ObjectType = ObjectType.Enemy;
        bullet.Velocity = direction.normalized * _speed;
        bullet.transform.position = _muzzle.position;
    }

    void IBossAttackTask.Initalize()
    {
        
    }
}
