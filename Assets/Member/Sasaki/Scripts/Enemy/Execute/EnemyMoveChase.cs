using UnityEngine;
using EnemyAISystem;

public class EnemyMoveChase : IEnemyMove
{
    [SerializeField] float _updateSpeed = 1.5f;

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
        return (_player.position - _user.position).normalized;
    }

    public void Initalize()
    {
        
    }
}
