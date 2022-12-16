using UnityEngine;

public class EffectExecuteTimer : IEffectExecutable
{
    [SerializeField] float _attributeTime;
    float _timer;

    void IEffectExecutable.SetEffect(GameObject effect, ParticleSystem particle)
    {
        
    }

    bool IEffectExecutable.Execute()
    {
        _timer += Time.deltaTime;
        return _timer > _attributeTime;
    }

    void IEffectExecutable.Initalize()
    {
        _timer = 0;
    }
}
