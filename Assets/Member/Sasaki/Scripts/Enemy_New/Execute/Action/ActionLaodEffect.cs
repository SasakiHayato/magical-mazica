using UnityEngine;
using BehaviourTree.Execute;

public class ActionLaodEffect : BehaviourAction
{
    [SerializeField] string _effectPath;
    [SerializeField] bool _onWait;

    bool _onLoad = false;

    Effect _effect;

    protected override bool Execute()
    {
        if (!_onLoad)
        {
            _onLoad = true;
            _effect = EffectStocker.Instance.LoadEffect(_effectPath, User.transform.transform);
        }

        if (_onWait)
        {
            return _effect.IsEnd;
        }
        else
        {
            return true;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        _effect = null;
        _onLoad = false;
    }
}
