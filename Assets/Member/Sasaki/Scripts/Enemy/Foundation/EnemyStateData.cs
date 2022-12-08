using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.Data;

public class EnemyStateData : IMonoDatableSystem<EnemyStateData>
{
    public Vector2 MoveDirection { get; set; }

    public (int, int) AttackAciveFrame { get; set; }

    public EnemyAISystem.EnemyAttackCollider AttackCollider { get; set; }
    public IBehaviourDatable IBehaviourDatable { get; set; }
    EnemyStateData IMonoDatableSystem<EnemyStateData>.GetData => this;
    string IMonoDatable.Path => nameof(EnemyStateData);
}
