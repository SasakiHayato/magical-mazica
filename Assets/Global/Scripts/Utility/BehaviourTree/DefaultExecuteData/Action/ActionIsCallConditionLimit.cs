using BehaviourTree;
using BehaviourTree.Execute;
using BehaviourTree.Data;
using UnityEngine;

/// <summary>
/// ConditionLimit���Ă΂ꂽ�̂������������Ƃ�ʒm���s��AI�s��
/// </summary>
public class ActionIsCallConditionLimit : BehaviourAction
{
    [SerializeField] int _id;
    BehaviourTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        BehaviourTreeUser treeUser = user.GetComponent<BehaviourTreeUser>();
        _userData = BehaviourTreeMasterData.Instance.FindUserData(treeUser.UserID);
    }

    protected override bool Execute()
    {
        _userData.IsCallLimit(_id);
        return true;
    }
}
