using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAddEventer : MonoBehaviour
{
    [System.Serializable]
    class ParticleData
    {
        [SerializeField] ParticleSystem _particle;
        [SerializeReference, SubclassSelector]
        List<IEffectExecutable> _executables;

        public int TaskCount => _executables.Count;

        public List<IEffectExecutable> EffectExecutableList => _executables;

        public void Setup()
        {
            _executables.ForEach(e => e.SetEffect(_particle.gameObject, _particle));
        }
    }

    [SerializeField] List<ParticleData> _particleDataList;

    int _endTaskCount = 0;
    int _maxTaskCount = 0;

    public bool IsEndTask => _endTaskCount >= _maxTaskCount;

    public void Setup()
    {
        _particleDataList.ForEach(p =>
        {
            p.Setup();
            _maxTaskCount += p.TaskCount;
        });
    }

    public void OnPlay()
    {
        _particleDataList
            .ForEach(p => p.EffectExecutableList
            .ForEach(p => StartCoroutine(OnProcess(p.Execute))));
    }

    IEnumerator OnProcess(System.Func<bool> func)
    {
        while (!func.Invoke())
        {
            yield return null;
        }

        _endTaskCount++;
    }

    public void Initalize()
    {
        _endTaskCount = 0;

        _particleDataList
            .ForEach(p => p.EffectExecutableList
            .ForEach(p => p.Initalize()));
    }
}
