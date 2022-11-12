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
    List<GameObject> _enemyObject;//敵のオブジェクト

    [SerializeField]
    List<Teleport> _teleporterList;//テレポートする場所のオブジェクト配列

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
