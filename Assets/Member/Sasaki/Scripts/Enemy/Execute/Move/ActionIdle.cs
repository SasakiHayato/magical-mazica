using UnityEngine;
using EnemyAISystem;

public class ActionIdle : IMove
{
    public void Setup(Transform user) { }

    public Vector2 Execute() => Vector2.zero;

    public void Initalize() { }
}
