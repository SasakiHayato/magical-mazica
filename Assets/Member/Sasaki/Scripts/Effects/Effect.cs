using UnityEngine;
using ObjectPool;
using ObjectPool.Event;

public interface IEffectExecutable
{
    void SetEffect(ParticleSystem particle);
    bool Execute();
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
