using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransform
{
    GameObject _enemyObj;
    Vector2 _position;
    public GameObject EnemyObj { get => _enemyObj; set => _enemyObj = value; }
    public Vector2 Position { get => _position; set => _position = value; }
    public EnemyTransform(GameObject enemyObj)
    {
        _enemyObj = enemyObj;
    }
}
