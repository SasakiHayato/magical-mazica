using UnityEngine;
using BehaviourTree.Execute;

public class ActionChase : BehaviourAction
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
        _behaviour.SetMoveDirection = (_player.position - User.transform.position).normalized * _updateSpeed;
        return true;
    }
}
