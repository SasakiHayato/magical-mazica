#if UNITY_EDITOR

using UnityEngine;
using BehaviourTree;
using BehaviourTree.Data;
using BehaviourTree.Execute;
using BehaviourTree.IO;

/// <summary>
/// DebugópÇÃAIèåè
/// </summary>
public class ConditionConsole : BehaviourConditional
{
    BehaviourTreeUserData _userData;

    protected override void Setup(GameObject user)
    {
        BehaviourTreeUser treeUser = user.GetComponent<BehaviourTreeUser>();

        _userData = BehaviourTreeMasterData.Instance.FindUserData(treeUser.UserID);

        BehaviourTreeDebug.SetLog(_userData, $"ConditionalSetUp. UserName{user.name}");
    }

    protected override bool Try()
    {
        BehaviourTreeDebug.SetLog(_userData, $"Try");

        return true;
    }

    protected override void Initialize()
    {
        BehaviourTreeDebug.SetLog(_userData, $"ConditionalInit.");
    }
}

#endif