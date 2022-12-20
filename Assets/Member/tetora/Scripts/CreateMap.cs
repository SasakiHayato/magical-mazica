using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport, Goal, Enemy
}
public enum WallType//床の位置が名前の由来
{
    None, NonFloar,
    Top, Right, Bottom, Left,//一方向だけ床
    TopRight, TopLeft, TopBottom,//二方向だけ床
    BottomLeft, BottomRight,
    RightLeft,
    NonTop, NonRight, NonBottom, NonLeft//三方向床
}
[System.Serializable]
public class CreateMap : MapCreaterBase
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
    int _releDis = 3;//何マス離すか
    TeleporterController _teleporterController;
    [SerializeField]
    int _createEnemyTime = 30;//敵生成のインターバル

    float _wallObjSize = 3;//マップ一つ一つのサイズ
    GameObject[] _enemies;
    List<GameObject> _stageObjList = new List<GameObject>();
    Dictionary<int, EnemyTransform> _enemyDic = new Dictionary<int, EnemyTransform>();//Key:ID Value:EnemyTransform
    StageMap _stageMap;
    int _startDigPos;//掘り始める始点

    public StageMap StageMap { get => _stageMap; }

    public static int TepoatObjLength => Instance._teleportObj.Length;

    public static CreateMap Instance { get; set; }

    public override Transform PlayerTransform { get; protected set; }

    protected override void Create()
    {
        Instance = this;
        InitialSet();
    }

    protected override void Initalize()
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
        _enemies = new GameObject[_scriptableObject.EnemyObject.Count];
        _enemyDic = new Dictionary<int, EnemyTransform>();
        for (int i = 0; i < _scriptableObject.EnemyObject.Count; i++)
        {
            _enemies[i] = _scriptableObject.EnemyObject[i].gameObject;
        }
        _teleporterController = new TeleporterController();
        //壁オブジェクトのScaleSizeを入れる
        _wallObjSize = _wallObj.transform.localScale.x;
        ResetMapFlag();
        StartDig();
        //CreateStage();
        DecisionPlayerPos();
        SetGoalPos();
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
    ///// <summary>掘り始める</summary>
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
    /// <summary>
    /// Stageの作成開始
    /// </summary>
    void CreateStage()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _stageMap)
        {
            if (pos.State == MapState.Floar)
            {
                var emptyObj = new GameObject();
                emptyObj.name = "Floar";
                _stageObjList.Add(emptyObj);
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                CheckAroundState(pos);
                CreateWallObj(pos);
            }
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
            GameObject enemy = Instantiate(_enemies[i]);
            var enebase = enemy.GetComponent<Enemy>();
            enebase.ID = i;
            _enemyDic.Add(enebase.ID, new EnemyTransform(enemy));
            _enemyDic[enebase.ID].IsCreate = false;
            DebugSetEnemyObject.SetEnemy(enemy.transform);
            SetEnemyPos(enemy, enebase.ID);
        }
    }
    /// <summary>
    /// 敵の生成する場所を決める
    /// </summary>
    /// <param name="enemy">敵のGameObject</param>
    void SetEnemyPos(GameObject enemy, int count)
    {
        int random = _stageMap.RandomFloarId();//ランダムな床のIDを取得し変数に格納
        var ene = enemy.GetComponent<Enemy>();
        if (_stageMap[random].IsGenerate != true)
        {
            SetEnemyPos(enemy, count);
        }
        else
        {
            if (_stageMap.CheckUnderDir(_stageMap[random], MapState.Floar))//下が床なら＝空中
            {
                if (ene.IsInstantiateFloat)
                {
                    enemy.transform.position = _stageMap[random, _wallObjSize];
                    _stageMap[random].State = MapState.Enemy;
                    _stageMap[random].IsGenerate = false;
                    _enemyDic[count].Position = enemy.transform.position;
                }
                else
                {
                    SetEnemyPos(enemy, count);
                }
            }
            else
            {
                enemy.transform.position = _stageMap[random, _wallObjSize];
                _stageMap[random].State = MapState.Enemy;
                _stageMap[random].IsGenerate = false;
                _enemyDic[count].Position = enemy.transform.position;
            }
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
    /// <summary>
    /// ゴールの位置を調整する
    /// </summary>
    void SetGoalPos()
    {
        var goal = Instantiate(_goalObj);
        goal.transform.SetParent(_parentObj.transform);
        Point goalPoint = Dijkstra.GetGoalPoint(_stageMap);
        goalPoint.State = MapState.Goal;
        goal.transform.position = goalPoint.ObjTransform.position;
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
            teleObj.transform.SetParent(_parentObj.transform);
            _teleporterController.CreateData(teleObj.transform, GetTeleportData);

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
    void ResetMapFlag()
    {
        foreach (var item in _stageMap)
        {
            item.IsGenerate = true;
        }
    }
    void GetTeleportData(int id)
    {
        Transform transform = _teleporterController.GetData(id);
        GameController.Instance.Player.position = transform.position;
    }
    /// <summary>
    /// Enemyが死んだときにFlagを変える
    /// </summary>
    /// <param name="enemy">死んだEnemy</param>
    public void DeadEnemy(GameObject enemy)//Enemyが死んだときに外部から呼ぶ
    {
        var ene = enemy.GetComponent<Enemy>();
        ChangeFlag(ene.ID);
        StartCoroutine(nameof(CreateEnemyCoroutine), ene.ID);
    }
    /// <summary>
    /// Flagを切り替える
    /// </summary>
    /// <param name="id">切り替えたい場所のID</param>
    void ChangeFlag(int id)
    {
        if (_enemyDic[id].IsCreate)
        {
            _enemyDic[id].IsCreate = false;
        }
        else
        {
            _enemyDic[id].IsCreate = true;
        }
    }
    /// <summary>
    /// 敵を生成する
    /// </summary>
    /// <param name="id">敵のID</param>
    public void CreateEnemy(int id)
    {
        GameObject enemyObj = Instantiate(_enemies[id]);
        var enebase = enemyObj.GetComponent<EnemyBase>();
        enebase.ID = id;
        enemyObj.transform.position = _enemyDic[id].Position;
        ChangeFlag(id);
    }
    IEnumerator CreateEnemyCoroutine(int id) //倒されたときにそこの場所だけカウントダウン開始
    {
        yield return new WaitForSeconds(_createEnemyTime);
        CreateEnemy(id);
    }
    /// <summary>
    /// 4方向の壁を調べる
    /// </summary>
    void CheckAroundState(Point pos)
    {
        if (_stageMap[pos.Id - _stageMap.MaxX].State == MapState.Wall || pos.Id - _stageMap.MaxX <= 0)//上が床
        {
            AddScore(pos, 1000);
        }
        if (_stageMap[pos.Id + 1].State == MapState.Wall || pos.Id + 1 >= _stageMap.MaxX)//右が床
        {
            AddScore(pos, 100);
        }
        if (_stageMap[pos.Id + _stageMap.MaxX].State == MapState.Wall || pos.Id + _stageMap.MaxX <= 0)//下が床
        {
            AddScore(pos, 10);
        }
        if (_stageMap[pos.Id - _stageMap.MaxX].State == MapState.Wall || pos.Id - _stageMap.MaxX <= 0)//左が床
        {
            AddScore(pos, 1);
        }
        JudgeScore(pos);
    }
    /// <summary>
    /// 設置するオブジェクトのScoreに加算
    /// </summary>
    /// <param name="pos">設置するオブジェクト</param>
    /// <param name="score">上が床なら+1000 右+100 下+10 左+1</param>
    void AddScore(Point pos, int score)
    {
        pos.JudgeScore += score;
    }
    void JudgeScore(Point pos)
    {
        switch (pos.JudgeScore)
        {
            case 1110://上、右、下が床
                pos.Type = WallType.NonLeft;
                break;
            case 1101://上、右、左が床
                pos.Type = WallType.NonBottom;
                break;
            case 1100://上、右が床
                pos.Type = WallType.TopRight;
                break;
            case 1011://上、下、左が床
                pos.Type = WallType.NonRight;
                break;
            case 1010://上、下が床
                pos.Type = WallType.TopBottom;
                break;
            case 1001://上、左が床
                pos.Type = WallType.TopLeft;
                break;
            case 1000://上だけ床
                pos.Type = WallType.Top;
                break;
            case 0111://右、下、左が床
                pos.Type = WallType.NonTop;
                break;
            case 0110://右、下が床
                pos.Type = WallType.BottomRight;
                break;
            case 0101://右、左が床
                pos.Type = WallType.RightLeft;
                break;
            case 0100://右が床
                pos.Type = WallType.Right;
                break;
            case 0011://下、左が床
                pos.Type = WallType.BottomLeft;
                break;
            case 0010://下が床
                pos.Type = WallType.Bottom;
                break;
            case 0001://左が床
                pos.Type = WallType.Left;
                break;
            case 0000:
                pos.Type = WallType.NonFloar;
                break;
        }
    }
    /// <summary>
    /// 作るオブジェクトの変更
    /// </summary>
    /// <param name="point"></param>
    /// <param name="wallType"></param>
    void CreateWallObj(Point point)
    {
        GameObject wallObj = _scriptableObject.GetParts(point.Type);
        Instantiate(wallObj);
        _stageObjList.Add(wallObj);
        SetTransform(wallObj, point);
    }
}

