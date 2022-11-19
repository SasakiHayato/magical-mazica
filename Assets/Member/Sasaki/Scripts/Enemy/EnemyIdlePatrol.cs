using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdlePatrol : IEnemyIdle
{
    Transform _user;

    public void Setup(Transform user)
    {
        _user = user;
    }

    public Vector2 OnMove()
    {
        return Vector2.right * Mathf.Sign(_user.localScale.x);
    }

    public void Initalize()
    {
        
    }
}
