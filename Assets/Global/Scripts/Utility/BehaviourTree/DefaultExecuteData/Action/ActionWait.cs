using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// 指定時間待たせるAI行動
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
