using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// Debug—p‚ÌğŒw’è‚µ‚½Bool‚ğ•Ô‚·
/// </summary>
public class ConditionTestAccess : BehaviourConditional
{
    [SerializeField] bool _isAccess;

    protected override bool Try()
    {
        return _isAccess;
    }
}
