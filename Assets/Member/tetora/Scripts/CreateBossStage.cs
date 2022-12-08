using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CreateBossStage : MapCreaterBase
{
    [SerializeField]
    Grid _parentGrid;
    [SerializeField]
    List<BossStageScriptable> _stageTipList;
    [SerializeField]
    Transform _createMapPos;//生成する場所
    [SerializeField]
    Transform _playerPos;
    [SerializeField]
    Transform _bossPos;
    [SerializeField]
    int _moveSize = 18;

    int _dataNum;//何個データ作ったか
    int _createdNum;//何個マップを作ったか
    bool _isCreated = false;//true:作れる false:作れない
    List<GameObject> _createdStageList = new List<GameObject>();

    public static CreateBossStage Instance;
    public override Transform PlayerTransform { get; protected set; }
    public int CreatedNum { get => _createdNum; set => _createdNum = value; }
    public bool IsCreated { get => _isCreated; set => _isCreated = value; }

    private void Awake()
    {
        Instance = this;
        InitialSet();
    }
    protected override void Create()
    {
        //Instance = this;
        //InitialSet();
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
    /// <summary>
    /// 最初にすること
    /// </summary>
    public void InitialSet()
    {
        CreateMap();
    }
    /// <summary>
    /// マップ生成
    /// </summary>
    public void CreateMap()
    {
        _dataNum++;
        int rnd = new System.Random().Next(0, _stageTipList.Count);//データ群の数から適当な値を取得
        GameObject parentObj = new GameObject();
        _createdStageList.Add(parentObj);
        parentObj.name = $"Stage:{_dataNum}";
        parentObj.transform.SetParent(_parentGrid.transform);
        for (int i = 0; i < _stageTipList[rnd].StageList.Count; i++)
        {
            GameObject stage = Instantiate(_stageTipList[rnd].StageList[i].gameObject, parentObj.transform);
            _createdNum++;
            SetMapTip(stage);
        }
    }
    /// <summary>
    /// Tagのついたオブジェクトの場所をセットする
    /// </summary>
    /// <param name="objTag">オブジェクトのタグ</param>
    /// <returns>Transform</returns>
    public Transform SetCharaPos(string objTag)
    {
        if (string.IsNullOrEmpty(objTag))
        {
            return null;
        }
        if (objTag == "Player")
        {
            return _playerPos;
        }
        if (objTag == "Boss")
        {
            return _bossPos;
        }
        return null;
    }
    /// <summary>
    /// ステージの場所変更
    /// </summary>
    /// <param name="mapTip"></param>
    void SetMapTip(GameObject mapTip)
    {
        mapTip.transform.position =
            new Vector2(_createMapPos.position.x - _moveSize, _createMapPos.position.y);
        _createMapPos = mapTip.transform;
    }
    /// <summary>
    /// いらなくなったマップの削除
    /// </summary>
    public void DestroyMap()
    {
        Destroy(_createdStageList.First());
        _createdStageList.RemoveAt(0);
    }
    public int CreateCount()
    {
        if (_dataNum == 1)
        {
            return CreatedNum - 1;
        }
        return CreatedNum;
    }
}





