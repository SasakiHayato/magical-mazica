using UnityEngine;
using BehaviourTree.Execute;

public class ActionPatrol : BehaviourAction
{
    [SerializeField] float _intervalTime;

    float _timer = 0;
    int _dirCollect = 1;

    IBehaviourDatable _behaviour;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _behaviour = user.GetComponent<IBehaviourDatable>();
    }

    protected override bool Execute()
    {
        _timer += Time.deltaTime;

        if (_timer > _intervalTime)
        {
            _dirCollect *= -1;
        }

        _behaviour.SetMoveDirection = Vector2.right * _dirCollect;
       
        return _timer > _intervalTime;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _timer = 0;
    }
}
