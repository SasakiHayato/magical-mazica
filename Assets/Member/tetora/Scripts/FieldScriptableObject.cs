using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FieldData")]
public class FieldScriptableObject : ScriptableObject
{
    [SerializeField]
    int _mapVerSide;//�c�̒���

    [SerializeField]
    int _mapHorSide;//���̒���

    [SerializeField]
    int _enemyCount;//�G�̐�

    [SerializeField]
    GameObject[] _enemyObject;//�G�̃I�u�W�F�N�g


    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public int EnemyCount { get => _enemyCount; }
    public GameObject[] EnemyObject { get => _enemyObject; }
}
