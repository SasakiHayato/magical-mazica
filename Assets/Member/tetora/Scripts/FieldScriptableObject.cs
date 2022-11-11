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
    List<TestTeleport> _teleportObj;//テレポートする場所のオブジェクト配列

    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public List<GameObject> EnemyObject { get => _enemyObject; }
}
[System.Serializable]
public class TestTeleport
{
    [SerializeField]
    List<GameObject> TeleportObjs = new List<GameObject>();
    public TestTeleport(List<GameObject> teleportObj)
    {
        TeleportObjs = teleportObj;
    }
}
