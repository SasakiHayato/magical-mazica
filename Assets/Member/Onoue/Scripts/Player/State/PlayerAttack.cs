using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;

public class PlayerAttack : MonoStateAttribute
{
    //Update
    public override void Execute()
    {
        Debug.Log("Execute PlayerAttack");
    }
    //ğŒ•ªŠò
    public override Enum Exit()
    {
        return Player.PlayerState.Attack;
    }
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEnable()
    {
        Debug.Log("Enable PlayerAttack");
    }
    //Awake
    public override void Setup()
    {
        Debug.Log("Setup PlayerAttack");
    }
}

