using UnityEngine;
using EnemyAISystem;

public class EnemyMoveBack : IEnemyMove
{
    [SerializeField] float _updateSpeed;

    Transform _player;
    Transform _user;

    float IEnemyMove.AttributeSpeed => _updateSpeed;

    public void Setup(Transform user)
    {
        _player = GameController.Instance.Player;
        _user = user;
    }

    public Vector2 OnMove()
    {
        return (_user.position - _player.position).normalized;
    }

    public void Initalize()
    {
        
    }
}
