using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecuteFade : IEffectExecutable
{
    [SerializeField] float _speed;

    float _timer = 0;

    SpriteRenderer _source;

    void IEffectExecutable.SetEffect(GameObject effect, ParticleSystem particle)
    {
        _source = effect.GetComponent<SpriteRenderer>();
    }

    bool IEffectExecutable.Execute()
    {
        _timer += Time.deltaTime;
        float alfa = Mathf.Lerp(1, 0, _timer * _speed);
        Color color = _source.color;
        _source.color = new Color(color.r, color.g, color.b, alfa);

        return alfa <= 0;
    }

    void IEffectExecutable.Initalize()
    {
        _timer = 0;
    }
}
