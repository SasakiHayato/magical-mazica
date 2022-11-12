using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;

public class PlayerRun : MonoStateAttribute
{
    public override void Execute()
    {
        Debug.Log("Execute PlayerRun");
    }

    public override Enum Exit()
    {
        return Player.PlayerState.Run;
    }

    public override void OnEnable()
    {
        Debug.Log("Enable PlayerRun");
    }

    public override void Setup()
    {
        Debug.Log("Setup PlayerRun");
    }
}

