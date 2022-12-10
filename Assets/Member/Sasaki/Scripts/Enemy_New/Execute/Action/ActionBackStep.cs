using UnityEngine;
using BehaviourTree.Execute;

using static RigidMasterData;

public class ActionBackStep : BehaviourAction
{
    [SerializeField] float _horizontalValue;
    [SerializeField] float _verticalValue;

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
        float x = (User.transform.position.x - _player.position.x);
        _behaviour.Rigid.SetImpulse(_horizontalValue * Mathf.Sign(x), ImpulseDirectionType.Horizontal, true);
        _behaviour.Rigid.SetImpulse(_verticalValue, ImpulseDirectionType.Vertical, true);
        return true;
    }
}
