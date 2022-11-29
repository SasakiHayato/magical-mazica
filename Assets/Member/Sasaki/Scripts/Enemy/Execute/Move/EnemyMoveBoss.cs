using UnityEngine;
using EnemyAISystem;

public class EnemyMoveBoss : IEnemyMove
{
    [SerializeField] float _updateSpeed;

    public float AttributeSpeed => _updateSpeed;


    public void Setup(Transform user)
    {
        
    }

    public Vector2 OnMove()
    {
        return Vector2.right * -1;
    }
    
    public void Initalize()
    {
        
    }
}
