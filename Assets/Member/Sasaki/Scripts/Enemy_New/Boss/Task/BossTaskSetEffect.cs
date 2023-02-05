using UnityEngine;

public class BossTaskSetEffect : IBossTask, IBossTaskOnEnableEventable
{
    [SerializeField] bool _onWait;
    [SerializeField] Transform _effectPosition;
    [SerializeField] string _effectPath;

    Effect _effect;

    void IBossTask.Setup(Transform user, Boss_NewData data)
    {
        
    }

    void IBossTaskOnEnableEventable.OnEnableEvent()
    {
        _effect = EffectStocker.Instance.LoadEffect(_effectPath, _effectPosition);
    }

    bool IBossTask.OnExecute()
    {
        if (_onWait)
        {
            return _effect.IsEnd;
        }
        else
        {
            return true;
        }
    }

    void IBossTask.Initalize()
    {
        
    }
}
