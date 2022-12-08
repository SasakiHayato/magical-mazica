using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// Debug�p�̏����w�肵��Bool��Ԃ�
/// </summary>
public class ConditionTestAccess : BehaviourConditional
{
    [SerializeField] bool _isAccess;

    protected override bool Try()
    {
        return _isAccess;
    }
}
