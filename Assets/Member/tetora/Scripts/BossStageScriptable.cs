using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossStage")]
public class BossStageScriptable : ScriptableObject
{
    [SerializeField]
    List<MapTip> _stageList = new List<MapTip>();
    [SerializeField]
    bool _firstCreatable; //true:Å‰‚Éì‚ê‚é false:Å‰‚Éì‚ê‚È‚¢
    public List<MapTip> StageList { get => _stageList; set => _stageList = value; }
    public bool FirstCreatable { get => _firstCreatable; }
}
