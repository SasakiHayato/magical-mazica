using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FieldData")]
public class FieldScriptableObject : ScriptableObject
{
    [SerializeField]
    int _mapVerSide;//c‚Ì’·‚³

    [SerializeField]
    int _mapHorSide;//‰¡‚Ì’·‚³

    [SerializeField]
    int _enemyCount;//“G‚Ì”

    [SerializeField]
    GameObject[] _enemyObject;//“G‚ÌƒIƒuƒWƒFƒNƒg


    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public int EnemyCount { get => _enemyCount; }
    public GameObject[] EnemyObject { get => _enemyObject; }
}
