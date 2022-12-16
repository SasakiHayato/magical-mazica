using UnityEngine;
using BehaviourTree.Execute;

public class ConditionObstacle : BehaviourConditional
{
    [SerializeField] LayerMask _hitLayer;

    protected override bool Try()
    {
        if (GameController.Instance.Player == null) return false;
        
        return !IsHit();
    }

    bool IsHit()
    {
        float distance = Vector2.Distance(User.transform.position, GameController.Instance.Player.position);
        return Physics2D.Raycast(User.transform.position, GameController.Instance.Player.position, distance, _hitLayer);
    }
}
