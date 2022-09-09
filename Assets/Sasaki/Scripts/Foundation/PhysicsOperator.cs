using UnityEngine;

/// <summary>
/// •¨—‹““®‚Ì‘€ìƒNƒ‰ƒX
/// </summary>

public class PhysicsOperator : MonoBehaviour
{
    [SerializeField] GroundData _groundData;

    float _timer;

    void Awake()
    {
        if (_groundData == null)
        {
            Debug.LogError("GrounData‚ª‚ ‚è‚Ü‚¹‚ñB");
        }
    }

    void Update()
    {
        
    }
}
