using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport, Goal
}
[System.Serializable]
public class CreateMap : MonoBehaviour, IGameDisposable, IGameSetupable
{
    [SerializeField]
    FieldScriptableObject _scriptableObject;
    [SerializeField]
    GameObject _wallObj;
    [SerializeField]
    GameObject _goalObj;
    [SerializeField]
    GameObject _parentObj;
    [SerializeField]
    GameObject[] _teleportObj;
    [SerializeField]
    int _mapScale = 14;
    [SerializeField]
    int _releDis = 3;//何マス離すか
    TeleporterController _teleporterController;
    [SerializeField]//teleporterを何セット置くか
    int _teleporterCount = 1;

    float _wallObjSize = 3;//マップ一つ一つのサイズ
    List<GameObject> _stageObjList = new List<GameObject>();
    StageMap _stageMap;
    int _startDigPos;//掘り始める始点
    public Transform PlayerTransform { get; private set; }
    public StageMap StageMap { get => _stageMap; }
    private void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
        GameController.Instance.AddGameDisposable(this);
    }
    public void GameSetup()
    {
        InitialSet();
    }
    public void GameDispose()
    {
        if (_parentObj != null)
        {
            foreach (Transform item in _parentObj.transform)//transformをforeachで回すと子オブジェクトが取ってこれる
            {
                Destroy(item.gameObject);
            }
        }
    }
    /// <summary>初期設定</summary>
    public void InitialSet()
    {
        _stageMap = new StageMap(_scriptableObject.MapHorSide, _scriptableObject.MapVerSide);
        _teleporterController = new TeleporterController();
        //壁オブジェクトのScaleSizeを入れる
        _wallObjSize = _wallObj.transform.localScale.x;
        StartDig();
        DecisionPlayerPos();
        InstantiateEnemy();
        InstantiateTeleObj();
    }
    /// <summary>ランダムな開始地点を決める</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_stageMap.MaxX - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_stageMap.MaxY - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _stageMap.MaxX;
    }
    /// <summary>4方向をランダムな順番で返す</summary>
    /// <param name="id">開始地点のid</param>
    /// <returns>4方向のid</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_stageMap.MaxX  * 2),id - _stageMap.MaxX ), //上
            (id + _stageMap.MaxX  * 2,id + _stageMap.MaxX ), //下
            (id + 2,id + 1), //右
            (id - 2,id - 1) //左
        };
        for (int i = 0; i < twoTargetDirs.Length; i++)
        {
            //順番をシャッフルする
            int r = new System.Random().Next(0, twoTargetDirs.Length);
            var tmp = twoTargetDirs[i];
            twoTargetDirs[i] = twoTargetDirs[r];
            twoTargetDirs[r] = tmp;
        }
        foreach ((int two, int one) dir in twoTargetDirs)
        {
            //two:2つ先のid ,one:1つ先のid
            if (dir.two < 0 || dir.two > _stageMap.Length - 1)//範囲内のidのみ値を返す
            {
                continue;
            }
            //横のサイズを超えないか
            if (id % _stageMap.MaxX < 1 || id % _stageMap.MaxX >= _stageMap.MaxX - 1)
            {
                continue;
            }
            //2個先が壁か確認
            if (_stageMap[dir.two].State != MapState.Wall)
            {
                continue;
            }
            //1個先が壁か確認
            if (_stageMap[dir.one].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _stageMap.MaxX == 0 || dir.two / _stageMap.MaxX == _stageMap.MaxY - 1)
            {
                continue;
            }
            if (dir.two % _stageMap.MaxX == 0 || dir.two % _stageMap.MaxX == _stageMap.MaxX - 1)
            {
                continue;
            }
            yield return dir;
        }
    }
    /// <summary>穴を掘る処理</summary>
    /// <param name="id">開始地点</param>
    void Dig(int id)
    {
        _stageMap[id].State = MapState.Floar;
        _stageMap[id].IsGenerate = true;
        foreach (var posId in CheckDir(id))
        {
            _stageMap[posId.oneTarget].State = MapState.Floar;
            Dig(posId.twoTarget);
        }
    }
    /// <summary>掘り始める</summary>
    void StartDig()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _stageMap)
        {
            if (pos.State == MapState.Floar)
            {
                var emptyObj = new GameObject();//空のオブジェクトを生成
                emptyObj.name = "Floar";
                _stageObjList.Add(emptyObj);
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                var wall = Instantiate(_wallObj);
                _stageObjList.Add(_wallObj);
                SetTransform(wall, pos);
            }
        }
    }
    /// <summary>オブジェクトを並べる</summary>
    /// <param name="obj">並べたいオブジェクト</param>
    /// <param name="map">並べたいMap</param>
    void SetTransform(GameObject obj, Point map)
    {
        obj.transform.SetParent(_parentObj.transform);
        obj.transform.position = _stageMap[map.Id, _wallObjSize];
        obj.name = $"ID:{map.Id}";
        map.ObjTransform = obj.transform;
    }
    /// <summary>
    /// 敵を種類の数だけ生成
    /// </summary>
    void InstantiateEnemy()
    {
        for (int i = 0; i < _scriptableObject.EnemyObject.Count; i++)
        {
            GameObject enemy = Instantiate(_scriptableObject.EnemyObject[i]);
            SetEnemyPos(enemy);
        }
    }
    /// <summary>
    /// 敵の生成する場所を決める
    /// </summary>
    /// <param name="enemy">敵のGameObject</param>
    void SetEnemyPos(GameObject enemy)
    {
        int random = _stageMap.RandomFloarId();//ランダムな床のIDを取得し変数に格納
        if (_stageMap[random].IsGenerate != true)
        {
            SetEnemyPos(enemy);
        }
        else
        {
            enemy.transform.position = _stageMap[random, _wallObjSize];
            _stageMap[random].IsGenerate = false;
        }
    }
    /// <summary>Player生成の場所を決める</summary>
    public void DecisionPlayerPos()
    {
        int rndId = _stageMap.RandomFloarId();//床オブジェクトの入っているListの要素数からランダムな値を取得
        if (rndId - _stageMap.MaxX >= 0 && _stageMap[rndId - _stageMap.MaxX].State == MapState.Wall)
        {
            _stageMap[rndId].State = MapState.Player;//Player生成する場所
            for (int i = 0; i < _releDis; i++)
            {
                foreach (var point in _stageMap.CheckDir(_stageMap[rndId], i))
                {
                    _stageMap[point].IsGenerate = false;
                }
            }
            PlayerTransform = _stageMap[rndId].ObjTransform;
            return;
        }
        DecisionPlayerPos();
    }

    void SetGoalPos()
    {
        //経路探索アルゴリズムを使ってプレイヤーから一番遠い所に生成
    }
    void InstantiateTeleObj()
    {
        int leftX = _stageMap.MaxX / 3;//3等分した時の左の線
        int rightX = _stageMap.MaxX / 3 * 2;//3等分した時の右の線
        int underY = _stageMap.MaxY / 3;//3等分した時の下の線
        int overY = _stageMap.MaxY / 3 * 2;//3等分した時の上の線
        List<Point> leftUpList = new List<Point>();//左上の床List
        List<Point> leftDownList = new List<Point>();//左下の床List
        List<Point> rightUpList = new List<Point>();//右上の床List
        List<Point> rightDownList = new List<Point>();//右下の床List
        foreach (var item in _stageMap.GetFloar())
        {
            int itemX = item.Id % _stageMap.MaxX;
            int itemY = item.Id / _stageMap.MaxX;
            if (_stageMap.CheckUnderDir(item, MapState.Wall))//下が壁の床を探してる
            {
                if (0 < itemX && itemX <= leftX)//左
                {
                    if (0 < itemY && itemY <= underY)//下
                    {
                        leftDownList.Add(item);
                    }
                    else if (overY < itemY && itemY < _stageMap.MaxY)//上
                    {
                        leftUpList.Add(item);
                    }
                }
                else if (rightX < itemX && itemX < _stageMap.MaxX)//右
                {
                    if (0 < itemY && itemY <= underY)//下
                    {
                        rightDownList.Add(item);
                    }
                    else if (overY < itemY && itemY < _stageMap.MaxY)//上
                    {
                        rightUpList.Add(item);
                    }
                }
            }
        }
        //オブジェクト生成
        for (int i = 0; i < _teleportObj.Length; i++)
        {
            var teleObj = Instantiate(_teleportObj[i]);
            switch (i)
            {
                case 0:
                    teleObj.transform.position = CanSetRandomPos(leftUpList).position;
                    break;
                case 1:
                    teleObj.transform.position = CanSetRandomPos(leftDownList).position;
                    break;
                case 2:
                    teleObj.transform.position = CanSetRandomPos(rightUpList).position;
                    break;
                case 3:
                    teleObj.transform.position = CanSetRandomPos(rightDownList).position;
                    break;
            }
        }
    }
    /// <summary>
    /// ランダムな場所の下が床ならTransformを返す
    /// </summary>
    /// <returns>オブジェクトのTransform</returns>
    Transform CanSetRandomPos(List<Point> points)
    {
        int rnd = new System.Random().Next(0, points.Count);
        points[rnd].State = MapState.Teleport;
        return points[rnd].ObjTransform;
    }
    public void GetTeleportData(int id)
    {
        _teleporterController.GetData(id);
    }
}

