using UnityEngine;

public class EffectExecuteScale : IEffectExecutable
{
    [SerializeField] float _executeSpeed = 1;
    [SerializeField] Vector2 _setScale;

    float _timer = 0;
    Vector2 _defaultScale;

    ParticleSystem _particle;

    void IEffectExecutable.SetEffect(ParticleSystem particle)
    {
        _defaultScale = particle.transform.localScale;
        _particle = particle;
    }

    bool IEffectExecutable.Execute()
    {
        _timer += Time.deltaTime * _executeSpeed;

        Vector2 scale = Vector2.Lerp(_defaultScale, _setScale, _timer);
        _particle.transform.localScale = scale;

        return _setScale == scale;
    }

    void IEffectExecutable.Initalize()
    {
        _particle.transform.localScale = _defaultScale;
        _timer = 0;
    }
}
