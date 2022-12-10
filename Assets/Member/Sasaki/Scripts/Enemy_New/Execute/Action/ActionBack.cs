using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

public class ActionBack : BehaviourAction
{
    [SerializeField] float _updateSpeed;

    Transform _player;
    IBehaviourDatable _behaviour;

    protected override void Setup(GameObject user)
    {
        base.Setup(user);
        _player = GameController.Instance.Player;
        _behaviour = user.GetComponent<IBehaviourDatable>();
    }

    protected override bool Execute()
    {
        _behaviour.SetMoveDirection = (User.transform.position - _player.position).normalized * _updateSpeed;
        return true;
    }
}
