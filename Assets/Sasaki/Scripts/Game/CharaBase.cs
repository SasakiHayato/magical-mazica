using UnityEngine;
using UniRx;
using CustomPhysics;

/// <summary>
/// Character�̊��N���X
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
    /// ������. Start�֐�
    /// </summary>
    protected abstract void Setup();

    /// <summary>
    /// ���񂾍ۂ̏���
    /// </summary>
    protected virtual void DeadEvent()
    {
        Destroy(gameObject);
    }
}
