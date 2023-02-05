using UnityEngine;
using Cysharp.Threading.Tasks;

public class BossTaskShotBullet : IBossTask, IBossTaskOnEnableEventable
{
    [SerializeField] float _delayTime;
    [SerializeField] Transform[] _muzzle;

    int _damage = 0;
    float _bulletSpeed = 0;
    bool _isEndTask = false;

    Transform _user;
    Bullet _bullet = null;

    void IBossTask.Setup(Transform user, Boss_NewData data)
    {
        _user = user;
        _bullet = data.Attack.BulletPrefab;
        _bulletSpeed = data.Attack.BulletSpeed;
        _damage = data.Attack.BulletDamage;
    }

    void IBossTaskOnEnableEventable.OnEnableEvent()
    {
        _isEndTask = false;
        OnDelayShot().Forget();
    }

    bool IBossTask.OnExecute()
    {
        return _isEndTask;
    }

    async UniTask OnDelayShot()
    {
        for (int index = 0; index < _muzzle.Length; index++)
        {
            Vector2 direction = GameController.Instance.Player.position - _user.position;

            Bullet bullet = Bullet.Init(_bullet, null, BulletType.Strike, _damage);
            bullet.transform.position = _muzzle[index].position;
            bullet.ObjectType = ObjectType.Enemy;
            bullet.Velocity = direction.normalized * _bulletSpeed;

            await UniTask.Delay(System.TimeSpan.FromSeconds(_delayTime));
        }

        _isEndTask = true;
    }

    void IBossTask.Initalize()
    {
        
    }
}
