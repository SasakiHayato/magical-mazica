using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ObjectPool;

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

    private void Start()
    {
        Instance = this;
        InitialSet();
    }
    protected override void Create()
    {
        Instance = this;
        InitialSet();
    }
    protected override void Initalize()
    {
        if (_parentGrid != null)
        {
            foreach (Transform item in _parentGrid.transform)
            {
                Destroy(item.gameObject);
            }
        }
    }
    public void InitialSet()
    {
        for (int i = 0; i < _stageTipList.Count; i++)
        {
            CreateRandMap(i);
        }
    }
    public void CreateMap()
    {
        int rnd = new System.Random().Next(0, _stageTipList.Count);
        GameObject mapTip = Instantiate(_stageTipList[rnd].gameObject, _parentGrid.transform);
        SetMapTip(mapTip);
    }
    void CreateRandMap(int i)
    {
        int rnd = new System.Random().Next(0, _stageTipList.Count);
        if (i == 0)
        {
            if (_stageTipList[rnd].GetComponent<MapTip>().IsFirstMake != true)
            {
                CreateRandMap(i);
            }
        }
        GameObject mapTip = Instantiate(_stageTipList[rnd].gameObject, _parentGrid.transform);
        SetMapTip(mapTip);
    }
    void SetMapTip(GameObject mapTip)
    {
        mapTip.transform.position =
            new Vector2(_createMapPos.position.x - _moveSize, _createMapPos.position.y);
        _createMapPos = mapTip.transform;
    }
}


