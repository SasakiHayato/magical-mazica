using UnityEngine;

public class EffectExecuteDefault : IEffectExecutable
{
    ParticleSystem _particle;

    void IEffectExecutable.SetEffect(ParticleSystem particle)
    {
        _particle = particle;
    }

    bool IEffectExecutable.Execute()
    {
        return !_particle.isPlaying;
    }

    void IEffectExecutable.Initalize()
    {
        
    }
}
