using BehaviourTree.Execute;

public class ActionDead : BehaviourAction
{
    protected override bool Execute()
    {
        User.GetComponent<IDamagable>().AddDamage(9999999);
        //Object.Destroy(User);
        return true;
    }
}
