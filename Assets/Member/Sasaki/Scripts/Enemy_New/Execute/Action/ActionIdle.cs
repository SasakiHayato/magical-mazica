using UnityEngine;
using BehaviourTree.Execute;

public class ActionIdle : BehaviourAction
{
    IBehaviourDatable _behaviour;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _behaviour = user.GetComponent<IBehaviourDatable>();
    }

    protected override bool Execute()
    {
        _behaviour.SetMoveDirection = Vector2.zero;
        return true;
    }
}
