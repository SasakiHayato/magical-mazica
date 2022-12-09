using UnityEngine;
using BehaviourTree;

public class Boss : EnemyBase
{
    [SerializeField] float _farDistance;
    [SerializeField] float _nearDistance;
    [SerializeField] float _updateSpeedRate;
    [SerializeField] float _downSpeedRate;
    
    Transform _core = null;

    protected override void Setup()
    {
        CreateCore();
    }

    void CreateCore()
    {
        _core = new GameObject("Core").transform;
        _core.position = transform.position;
        _core.localScale = Vector2.one / transform.localScale.x;
        _core.SetParent(transform);
    }

    protected override void Execute()
    {
        MoveSystem();
    }

    void MoveSystem()
    {
        float speed = Speed;
        float distance = Mathf.Abs(_core.position.x) - Mathf.Abs(GameController.Instance.Player.position.x);
        
        if (Mathf.Abs(distance) > _farDistance)
        {
            speed *= _updateSpeedRate;
        }

        if (Mathf.Abs(distance) < _nearDistance)
        {
            speed /= _downSpeedRate;
        }

        Rigid.SetMoveDirection = Vector2.right * -1 * speed;
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }
}
