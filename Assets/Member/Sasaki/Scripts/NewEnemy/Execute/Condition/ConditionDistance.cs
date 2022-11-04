using UnityEngine;
using EnemyAISystem;

public class ConditionDistance : ICondition
{
    enum AttributeType
    {
        In,
        Out,
    }

    [SerializeField] AttributeType _type;
    [SerializeField] float _distance;

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
            Debug.Log("Player‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½BConditionDistance");
            _player = user;
        }
    }

    public bool Try()
    {
        float dist = Vector2.Distance(_user.position, _player.position);
        return _type == AttributeType.In ? dist > _distance : dist < _distance;
    }

    public void Initalize() { }
}
