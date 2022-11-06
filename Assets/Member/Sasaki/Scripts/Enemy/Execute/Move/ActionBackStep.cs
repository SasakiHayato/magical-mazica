using UnityEngine;
using EnemyAISystem;

public class ActionBackStep : IMove
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
        else
        {
            Debug.Log("PlayerÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅBConditionDistance");
            _player = user;
        }
    }

    public Vector2 Execute()
    {
        float dirX = _user.position.x - _player.position.x;

        return Vector2.right * Mathf.Sign(dirX);
    }

    public void Initalize()
    {
        
    }
}
