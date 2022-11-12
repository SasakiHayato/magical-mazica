using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
public class PlayerIdle : MonoStateAttribute
{
    public override void Execute()
    {
        Debug.Log("Execute PlayerIdle");
    }

    public override Enum Exit()
    {
        return Player.PlayerState.Idle;
    }

    public override void OnEnable()
    {
        Player player = UserRetentionData.GetData<Player>(nameof(Player));
    }

    public override void Setup()
    {
        Debug.Log("PlayerIdle");
    }
}

