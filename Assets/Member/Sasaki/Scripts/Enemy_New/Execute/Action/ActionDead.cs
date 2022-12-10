using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

public class ActionDead : BehaviourAction
{
    protected override bool Execute()
    {
        Object.Destroy(User);
        return true;
    }
}
