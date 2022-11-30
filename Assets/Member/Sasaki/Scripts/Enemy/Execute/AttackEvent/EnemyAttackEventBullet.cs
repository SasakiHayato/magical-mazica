using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAISystem;

public class EnemyAttackEventBullet : IEnemyAttackEvent
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] float _shotIntervalTime;

    float _timer = 0;

    Transform _user;
    Transform _player;
    Sprite _source;

    public void Setup(Transform user)
    {
        _user = user;
        _player = GameController.Instance.Player;
        _source = _bulletPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void EnableEvent()
    {
        
    }

    public void ExecuteEvent()
    {
        _timer += Time.deltaTime;

        if (_timer > _shotIntervalTime)
        {
            _timer = 0;
            Bullet bullet = Bullet.Init(_bulletPrefab, _source, BulletType.Strike, _damage);
            bullet.transform.position = _user.position;
            bullet.Velocity = (_player.position - _user.position).normalized * _speed;
        }
    }

    public void EndEvent()
    {
        _timer = 0;
    }
}
