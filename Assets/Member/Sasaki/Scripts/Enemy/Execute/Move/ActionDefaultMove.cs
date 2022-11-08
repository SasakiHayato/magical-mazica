using UnityEngine;
using EnemyAISystem;

public class ActionDefaultMove : IMove
{
    FieldTouchOperator _fieldTouchOperator;

    int _dirCollect = 1;

    public void Setup(Transform user) 
    {
        _fieldTouchOperator = user.GetComponentInChildren<FieldTouchOperator>();
    }

    public Vector2 Execute()
    {
        if (!_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground)
            || _fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall))
        {
            _fieldTouchOperator.ChangeDirCollect();
            _dirCollect *= -1;
        }

        return Vector2.right * _dirCollect;
    }

    public void Initalize() { }
}
