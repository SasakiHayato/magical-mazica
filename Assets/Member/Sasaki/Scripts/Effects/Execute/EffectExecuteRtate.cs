using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecuteRtate : IEffectExecutable
{
    [SerializeField] float _speed;

    float _timer = 0;

    Transform _effect;

    void IEffectExecutable.SetEffect(GameObject effect, ParticleSystem particle)
    {
        _effect = effect.transform;
    }

    bool IEffectExecutable.Execute()
    {
        _timer += Time.deltaTime * _speed;

        float angle = Mathf.Lerp(0, 360, _timer);
        _effect.rotation = Quaternion.Euler(0, 0, angle);

        return angle >= 360;
    }

    void IEffectExecutable.Initalize()
    {
        _timer = 0;
    }
}
