using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;

public class PlayerJump : MonoStateAttribute
{
    public override void Execute()
    {
        Debug.Log("Execute PlayerJump");
    }

    public override Enum Exit()
    {
        return Player.PlayerState.Jump;
    }

    public override void OnEnable()
    {
        Debug.Log("Enable PlayerJump");
    }

    public override void Setup()
    {
        Debug.Log("Setup PlayerJump");
    }
}
