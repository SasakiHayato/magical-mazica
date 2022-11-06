using UnityEngine;
using ObjectPool;
using ObjectPool.Event;

public class Effecter : MonoBehaviour, IPool, IPoolOnEnableEvent
{
    ParticleSystem _particle;

    void IPool.Setup(Transform parent)
    {
        _particle = GetComponent<ParticleSystem>();
        _particle.Stop();
    }

    void IPoolOnEnableEvent.OnEnableEvent()
    {
        _particle.Play();
    }

    bool IPool.Execute()
    {
        return !_particle.isPlaying;
    }
}
