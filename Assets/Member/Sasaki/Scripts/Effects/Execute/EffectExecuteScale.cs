using UnityEngine;

public class EffectExecuteScale : IEffectExecutable
{
    [SerializeField] float _executeSpeed = 1;
    [SerializeField] Vector2 _setScale;

    float _timer = 0;
    Vector2 _defaultScale;

    Transform _effect;
    
    void IEffectExecutable.SetEffect(GameObject effect, ParticleSystem particle)
    {
        _defaultScale = effect.transform.localScale;
        _effect = effect.transform;
    }

    bool IEffectExecutable.Execute()
    {
        _timer += Time.deltaTime * _executeSpeed;

        Vector2 scale = Vector2.Lerp(_defaultScale, _setScale, _timer);
        _effect.localScale = scale;

        return _setScale == scale;
    }

    void IEffectExecutable.Initalize()
    {
        _effect.localScale = _defaultScale;
        _timer = 0;
    }
}
