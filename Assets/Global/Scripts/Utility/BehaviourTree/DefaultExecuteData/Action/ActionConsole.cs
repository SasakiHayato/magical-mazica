#if UNITY_EDITOR

using UnityEngine;
using BehaviourTree;
using BehaviourTree.Execute;
using BehaviourTree.Data;
using BehaviourTree.IO;

/// <summary>
/// DebugópÇÃAIçsìÆ
/// </summary>
public class ActionConsole : BehaviourAction
{
    [SerializeField] string _txt;

    BehaviourTreeUserData _userData;

    protected override void Setup(GameObject user)
    {
        BehaviourTreeUser treeUser = user.GetComponent<BehaviourTreeUser>();
        _userData = BehaviourTreeMasterData.Instance.FindUserData(treeUser.UserID);

        BehaviourTreeDebug.SetLog(_userData, $"SetUpAction. UserName {_userData.UserPath}");
    }

    protected override bool Execute()
    {
        BehaviourTreeDebug.SetLog(_userData, $"Execute\n{_txt}");

        return true;
    }

    protected override void Initialize()
    {
        BehaviourTreeDebug.SetLog(_userData, $"ActionInit. User_{_userData.UserPath}");
    }
}

#endif