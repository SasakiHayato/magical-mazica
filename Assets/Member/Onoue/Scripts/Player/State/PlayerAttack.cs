using System.Collections.Generic;
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
        SoundManager.PlayRequestRandom(SoundSystem.SoundType.SEPlayer, "Attack");

        if (_stateData.ReadAttackType == PlayerStateData.AttackType.Default)
        {
            DefaultAttack();
        }
        else
        {
            _anim.OnPlay("Mazic");
        }
    }

    void DefaultAttack()
    {
        AnimOperator.AnimEvent anim = new AnimOperator.AnimEvent
        {
            Frame = 1,
            Event = () => _stateData.AttackCollider.SetActive(true),
        };

        AnimOperator.AnimEvent anim2 = new AnimOperator.AnimEvent
        {
            Frame = 4,
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
        _stateData.Rigid.SetMoveDirection = UnityEngine.Vector2.zero;
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

