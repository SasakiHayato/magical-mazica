using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "BossStage")]
public class BossStageScriptable : ScriptableObject
{
    [SerializeField]
    List<Tilemap> _stageList = new List<Tilemap>();
    [SerializeField]
    bool _firstCreatable; //true:最初に作れる false:最初に作れない
    public List<Tilemap> StageList { get => _stageList; set => _stageList = value; }
    public bool FirstCreatable { get => _firstCreatable; }
}
