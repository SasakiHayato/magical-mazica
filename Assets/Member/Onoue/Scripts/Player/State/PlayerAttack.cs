using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerAttack : MonoStateBase
{
    AnimOperator _anim;
    PlayerStateData _stateData;
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        AnimOperator.AnimEvent anim = new AnimOperator.AnimEvent
        {
            Frame = 4,
            Event = () => _stateData.AttackCollider.SetActive(true),
        };

        AnimOperator.AnimEvent anim2 = new AnimOperator.AnimEvent
        {
            Frame = 6,
            Event = () => _stateData.AttackCollider.SetActive(false),
        };

        List<AnimOperator.AnimEvent> list = new List<AnimOperator.AnimEvent>();
        list.Add(anim);
        list.Add(anim2);

        _anim.OnPlay("Attack", list);
    }
    //Update
    public override void OnExecute()
    {

    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_anim.EndCurrentAnim)
        {
            return ReturneState();
        }

        return Player.PlayerState.Attack;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
        _stateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));

    }
}

