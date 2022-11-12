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
    List<GameObject> _enemyObject;//�G�̃I�u�W�F�N�g

    [SerializeField]
    List<Teleport> _teleporterList;//�e���|�[�g����ꏊ�̃I�u�W�F�N�g�z��

    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public List<GameObject> EnemyObject { get => _enemyObject; }
    public List<Teleport> TeleporterList { get => _teleporterList; }
}
[System.Serializable]
public class Teleport
{
    [SerializeField]
    List<GameObject> teleportObjs = new List<GameObject>();
    public List<GameObject> TeleportObjs { get => teleportObjs; }
    public Teleport(List<GameObject> teleportObj)
    {
        teleportObjs = teleportObj;
    }
}
