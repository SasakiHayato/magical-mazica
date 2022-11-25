using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBossStage : MapCreaterBase
{
    [SerializeField]
    List<GameObject> _stageTipList;
    public static CreateBossStage Instance;
    public override Transform PlayerTransform { get; protected set; }

    protected override void Create()
    {
        Instance = this;
    }
    protected override void Initalize()
    {

    }
    public void InitialSet()
    {

    }
    /// <summary>
    /// スタート時にマップを生成する
    /// </summary>
    void CreateStartMap()
    {

    }
}


