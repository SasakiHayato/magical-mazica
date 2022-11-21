using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerAttack : MonoStateBase
{
    //State���ς��x�ɌĂ΂��
    public override void OnEntry()
    {
        Debug.Log("Entry PlayerAttack");
    }
    //Update
    public override void OnExecute()
    {
        Debug.Log("Execute PlayerAttack");
    }
    //��������
    public override Enum OnExit()
    {
        return Player.PlayerState.Attack;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        
    }
}

