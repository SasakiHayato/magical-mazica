using UnityEngine;
using ObjectPool;

public class Effecter : MonoBehaviour, IPool
{
    ParticleSystem _particle;

    void IPool.Setup(Transform parent)
    {
        _particle = GetComponent<ParticleSystem>();
        _particle.Stop();
    }

    void IPool.OnEnableEvent()
    {
        _particle.Play();
    }

    bool IPool.Execute()
    {
        return !_particle.isPlaying;
    }
}
