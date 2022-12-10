using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

public class ActionWaitAttack : BehaviourAction
{
    IBehaviourDatable _behaviour;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);

        _behaviour = user.GetComponent<IBehaviourDatable>();
    }
    protected override bool Execute()
    {
        return !_behaviour.OnAttack;
    }
}
