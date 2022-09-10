using UnityEngine;
using UniRx;
using CustomPhysics;

/// <summary>
/// Character‚ÌŠî’êƒNƒ‰ƒX
/// </summary>

[RequireComponent(typeof(PhysicsOperator))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] StatusData _statusData;

    protected PhysicsOperator PhysicsOperator { get; private set; } 

    void Start()
    {
        _statusData.Initalize();
        SubScribe();

        PhysicsOperator = GetComponent<PhysicsOperator>();

        Setup();
    }

    void SubScribe()
    {
        _statusData.ObservableHP
            .Select(hp => hp <= 0)
            .Subscribe(_ => DeadEvent())
            .AddTo(this);
    }

    /// <summary>
    /// ‰Šú‰». StartŠÖ”
    /// </summary>
    protected abstract void Setup();

    /// <summary>
    /// €‚ñ‚¾Û‚Ìˆ—
    /// </summary>
    protected virtual void DeadEvent()
    {
        Destroy(gameObject);
    }
}
