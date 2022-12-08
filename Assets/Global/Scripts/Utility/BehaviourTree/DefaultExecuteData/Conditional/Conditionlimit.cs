using UnityEngine;
using BehaviourTree;
using BehaviourTree.Execute;
using BehaviourTree.Data;

/// <summary>
/// ‰ñ”§ŒÀ‚Ì‚ ‚éAIs“®‚É‚Â‚¯‚éğŒ
/// </summary>
public class Conditionlimit : BehaviourConditional
{
    [SerializeField] int _limitID;
    BehaviourTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        BehaviourTreeUser treeUser = user.GetComponent<BehaviourTreeUser>();
        _userData = BehaviourTreeMasterData.Instance.FindUserData(treeUser.UserID);
    }

    protected override bool Try()
    {
        return _userData.IsLimitCondition(_limitID);
    }
}
