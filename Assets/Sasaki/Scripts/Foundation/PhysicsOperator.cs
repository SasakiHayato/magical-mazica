using UnityEngine;

/// <summary>
/// ���������̑���N���X
/// </summary>

public class PhysicsOperator : MonoBehaviour
{
    [SerializeField] GroundData _groundData;

    float _timer;

    void Awake()
    {
        if (_groundData == null)
        {
            Debug.LogError("GrounData������܂���B");
        }
    }

    void Update()
    {
        
    }
}
