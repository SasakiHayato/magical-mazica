using UnityEngine;

public class EnemyMoveChase : IEnemyMove
{
    Transform _player;
    Transform _user;

    float IEnemyMove.AttributeSpeed => 1.5f;

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
