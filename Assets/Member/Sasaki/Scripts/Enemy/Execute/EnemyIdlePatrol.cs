using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAISystem;

public class EnemyIdlePatrol : IEnemyIdle
{
    float _dirCollect = 1;

    FieldTouchOperator _fieldTouchOperator;
    
    public void Setup(Transform user)
    {
        _fieldTouchOperator = user.GetComponentInChildren<FieldTouchOperator>();
        _dirCollect = Mathf.Sign(user.localScale.x);
    }

    public Vector2 OnMove()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall)
            || !_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            _dirCollect *= -1;
            _fieldTouchOperator.OnChangeLayDir();
        }

        return Vector2.right * _dirCollect;
    }

    public void Initalize()
    {
        
    }
}
