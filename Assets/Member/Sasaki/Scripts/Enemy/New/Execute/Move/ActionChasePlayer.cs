using UnityEngine;
using EnemyAISystem;

public class ActionChasePlayer : IMove
{
    Transform _user;
    Transform _player;

    public void Setup(Transform user)
    {
        _user = user;

        if (GameManager.Instance != null && GameManager.Instance.GetPlayer() != null)
        {
            _player = GameManager.Instance.GetPlayer().transform;
        }
    }

    public Vector2 Execute()
    {
        Vector2 dir = _player.position - _user.position;
        dir.x = 1 * Mathf.Sign(dir.x);
        return dir;
    }

    public void Initalize() { }
}
