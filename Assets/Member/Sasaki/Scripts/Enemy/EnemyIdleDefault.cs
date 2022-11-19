using UnityEngine;

public class EnemyIdleDefault : IEnemyIdle
{
    public void Setup(Transform user) { }
    public Vector2 OnMove() => Vector2.zero;
    public void Initalize() { }   
}
