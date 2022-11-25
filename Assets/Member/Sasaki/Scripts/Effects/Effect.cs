using UnityEngine;
using ObjectPool;
using ObjectPool.Event;

public interface IEffectExecutable
{
    /// <summary>
    /// Setup
    /// </summary>
    /// <param name="particle">‘ÎÛEffect</param>
    void SetEffect(ParticleSystem particle);
    /// <summary>
    /// Update
    /// </summary>
    /// <returns>I—¹True‚ğ•Ô‚·</returns>
    bool Execute();
    /// <summary>
    /// ŒÄ‚Î‚ê‚é‚½‚Ñ‚É‰Šú‰»
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
