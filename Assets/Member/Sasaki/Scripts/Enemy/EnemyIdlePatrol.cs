using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdlePatrol : IEnemyIdle
{
    float _dirCollect = 1;
    FieldTouchOperator _fieldTouchOperator;

    public void Setup(Transform user)
    {
        _fieldTouchOperator = user.GetComponentInChildren<FieldTouchOperator>();
    }

    public Vector2 OnMove()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall)
            || !_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            _dirCollect *= -1;
        }

        return Vector2.right * _dirCollect;
    }

    public void Initalize()
    {
        
    }
}
