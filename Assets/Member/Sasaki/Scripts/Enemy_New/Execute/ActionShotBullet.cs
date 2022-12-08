using UnityEngine;
using BehaviourTree.Execute;

public class ActionShotBullet : BehaviourAction
{
    [SerializeField] Bullet _bullrtPrefab;

    Sprite _souce;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _souce = _bullrtPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    protected override bool Execute()
    {
        Bullet bullet = Bullet.Init(_bullrtPrefab, _souce, BulletType.Strike, 1);
        Vector2 direction = (GameController.Instance.Player.position - User.transform.position);
        bullet.transform.position = User.transform.position;
        bullet.ObjectType = ObjectType.Enemy;
        bullet.Velocity = direction.normalized * 10;
        return true;
    }
}
