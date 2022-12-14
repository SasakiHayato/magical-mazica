using UnityEngine;
using BehaviourTree.Execute;

public class ConditionDistance : BehaviourConditional
{
    enum AttributeType
    { 
        Out,
        In,
    }

    [SerializeField] AttributeType _attributeType;
    [SerializeField] float _attributeDistance;

    protected override bool Try()
    {
        if (GameController.Instance.Player == null) return false;

        switch (_attributeType)
        {
            case AttributeType.Out: 
                return _attributeDistance < Vector2.Distance(GameController.Instance.Player.position, User.transform.position);
            case AttributeType.In:
               return _attributeDistance > Vector2.Distance(GameController.Instance.Player.position, User.transform.position);
        }

        return false;
    }
}
