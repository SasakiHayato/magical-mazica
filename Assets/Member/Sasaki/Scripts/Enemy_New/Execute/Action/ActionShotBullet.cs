using UnityEngine;
using BehaviourTree.Execute;

public class ActionShotBullet : BehaviourAction
{
    [SerializeField] Transform _muzzle;
    [SerializeField] Bullet _bullrtPrefab;

    Sprite _souce;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _souce = _bullrtPrefab.GetComponent<SpriteRenderer>().sprite;

        if (_muzzle == null)
        {
            _muzzle = User.transform;
        }
    }

    protected override bool Execute()
    {
        Bullet bullet = Bullet.Init(_bullrtPrefab, _souce, BulletType.Strike, 1);
        Vector2 direction = (GameController.Instance.Player.position - User.transform.position);
        bullet.transform.position = _muzzle.position;
        bullet.ObjectType = ObjectType.Enemy;
        bullet.Velocity = direction.normalized * 10;
        return true;
    }
}
