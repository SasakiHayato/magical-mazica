using UnityEngine;
using EnemyAISystem;

public class ConditionEmpty : ICondition
{
    public void Setup(Transform user) { }

    public bool Try() => false;

    public void Initalize() { }
}
