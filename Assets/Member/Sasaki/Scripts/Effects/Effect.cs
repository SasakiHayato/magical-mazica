using UnityEngine;
using ObjectPool;
using ObjectPool.Event;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IEffectExecutable
{
    /// <summary>
    /// Setup
    /// </summary>
    /// <param name="particle">‘ÎÛEffect</param>
    void SetEffect(GameObject effect, ParticleSystem particle);
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
    [SerializeField] EffectAddEventer _addEventer = null;
    [SerializeReference, SubclassSelector]
    List<IEffectExecutable> _executableList;

    int _endCount = 0;

    ParticleSystem _particle;

    public bool IsEnd { get; private set; } = false;
    
    void IPool.Setup(Transform parent)
    {
        _particle = GetComponent<ParticleSystem>();
        _executableList.ForEach(e => e.SetEffect(gameObject, _particle));

        if (_addEventer != null)
        {
            _addEventer.Setup();
        }
    }

    void IPoolOnEnableEvent.OnEnableEvent()
    {
        IsEnd = false;
        _endCount = 0;
        _executableList.ForEach(e => e.Initalize());
        
        if (_addEventer != null)
        {
            _addEventer?.Initalize();

        }

        if (_particle != null)
        {
            _particle.Play();
        }

        if (_addEventer != null)
        {
            _addEventer.OnPlay();
        }

        foreach (IEffectExecutable effectable in _executableList)
        {
            StartCoroutine(OnProcess(effectable.Execute));
        }
    }

    bool IPool.Execute()
    {
        IsEnd = _addEventer != null ? _endCount >= _executableList.Count && _addEventer.IsEndTask : _endCount >= _executableList.Count;
        
        return IsEnd;
    }

    IEnumerator OnProcess(Func<bool> func)
    {
        while (!func.Invoke())
        {
            yield return null;
        }

        _endCount++;
    }
}
