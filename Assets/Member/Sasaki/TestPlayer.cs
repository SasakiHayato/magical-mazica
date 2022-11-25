using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 _power;
    
    RigidOperator _rigid;

    void Start()
    {
        _rigid = GetComponent<RigidOperator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            _rigid.SetImpulse(_power.y, RigidMasterData.ImpulseDirectionType.Vertical, true);
            _rigid.SetImpulse(_power.x, RigidMasterData.ImpulseDirectionType.Horizontal);
        }

        _rigid.SetMoveDirection = new Vector2(x * _speed, 0);
    }
}
