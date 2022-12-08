using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// �w�莞�ԑ҂�����AI�s��
/// </summary>
public class ActionWait : BehaviourAction
{
    [SerializeField] float _waitTime;

    float _timer;
    protected override bool Execute()
    {
        _timer += Time.deltaTime;
        
        return _timer > _waitTime;
    }

    protected override void Initialize()
    {
        _timer = 0;
    }
}
