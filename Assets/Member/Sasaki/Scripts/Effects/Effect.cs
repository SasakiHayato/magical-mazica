using UnityEngine;
using ObjectPool;
using ObjectPool.Event;

public interface IEffectExecutable
{
    /// <summary>
    /// Setup
    /// </summary>
    /// <param name="particle">�Ώ�Effect</param>
    void SetEffect(ParticleSystem particle);
    /// <summary>
    /// Update
    /// </summary>
    /// <returns>�I����True��Ԃ�</returns>
    bool Execute();
    /// <summary>
    /// �Ă΂�邽�тɏ�����
    /// </summary>
    void Initalize();
}

public class Effect : MonoBehaviour, IPool, IPoolOnEnableEvent
{
    [SerializeReference, SubclassSelector]
    IEffectExecutable _executable;

    ParticleSystem _particle;

    void IPool.Setup(Transform parent)
    {
        _particle = GetComponent<ParticleSystem>();
        _executable.SetEffect(_particle);
    }

    void IPoolOnEnableEvent.OnEnableEvent()
    {
        _executable.Initalize();
        _particle.Play();
    }

    bool IPool.Execute()
    {
        return _executable.Execute();
    }
}
