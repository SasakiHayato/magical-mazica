using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBossStage : MapCreaterBase
{
    [SerializeField]
    Grid _parentGrid;
    [SerializeField]
    List<Tilemap> _stageTipList;
    [SerializeField]
    Transform _createMapPos;//ê∂ê¨Ç∑ÇÈèÍèä
    [SerializeField]
    int _startCreateNum = 5;
    [SerializeField]
    int _moveSize = 18;

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
    void CreateMap()
    {

    }
}


