using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FieldData")]
public class FieldScriptableObject : ScriptableObject
{
    [SerializeField]
    int _mapVerSide;//縦の長さ

    [SerializeField]
    int _mapHorSide;//横の長さ

    [SerializeField]
    int _enemyCount;//敵の数

    [SerializeField]
    GameObject[] _enemyObject;//敵のオブジェクト


    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public int EnemyCount { get => _enemyCount; }
    public GameObject[] EnemyObject { get => _enemyObject; }
}
