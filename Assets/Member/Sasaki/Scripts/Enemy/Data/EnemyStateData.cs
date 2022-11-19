using UnityEngine;
using MonoState.Data;

[System.Serializable]
public class EnemyStateData : IMonoDatableSystem<EnemyStateData>
{
    [SerializeField] int _a;

    EnemyStateData IMonoDatableSystem<EnemyStateData>.GetData => this;
    string IMonoDatable.Path => nameof(EnemyStateData);

    public Vector2 MoveDir { get; set; }

    public float PlayerDistance { get; set; }
}
