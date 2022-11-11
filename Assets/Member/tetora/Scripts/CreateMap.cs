using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport
}
[System.Serializable]
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    FieldScriptableObject _scriptableObject;
    [SerializeField]
    GameObject _wallObj;
    [SerializeField]
    GameObject _parentObj;
    [SerializeField]
    int _mapScale = 14;
    [SerializeField]
    int _releDis = 3;//何マス離すか

    float _wallObjSize = 3;//マップ一つ一つのサイズ
    StageMap _stageMap;
    int _startDigPos;//掘り始める始点
    public Transform PlayerTransform { get; private set; }
    /// <summary>初期設定</summary>
    public void InitialSet()
    {
        _stageMap = new StageMap(_scriptableObject.MapHorSide, _scriptableObject.MapVerSide);
        //壁オブジェクトのScaleSizeを入れる
        _wallObjSize = _wallObj.transform.localScale.x;
        StartDig();
        DecisionPlayerPos();
        InstantiateEnemy();
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
            //Debug.Log($"stageMap:{_stageMap[posId.oneTarget].State}");
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
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                var wall = Instantiate(_wallObj);
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
                                              // Map rndMap = GetFloar()[rndId];//床オブジェクトのランダムなオブジェクトを取得
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

    //void SetTeleportPos(GameObject[] teleporterObj)
    //{
    //    if (TESTTELEPORT != null)
    //    {
    //        return;
    //    }
    //    List<Map> leftMapsList = new List<Map>();
    //    List<Map> rightMapsList = new List<Map>();
    //    int leftLine = _stageMap.maxX  / 3; //3等分した時の左の線
    //    int rightLine = _stageMap.maxX  / 3 * 2;//3等分下時の右の線
    //    foreach (var item in GetFloar())
    //    {
    //        if (item.Id % _scriptableObject.MapHorSide <= leftLine)
    //        {
    //            leftMapsList.Add(item);
    //        }
    //        if (item.Id % _scriptableObject.MapHorSide >= rightLine)
    //        {
    //            rightMapsList.Add(item);
    //        }
    //    }
    //    int leftRnd = new System.Random().Next(0, leftMapsList.Count);
    //    int rightRnd = new System.Random().Next(0, rightMapsList.Count);
    //    var leftTele = Instantiate(TESTTELEPORT[0]);
    //    var rightTele = Instantiate(TESTTELEPORT[1]);
    //    leftTele.transform.position = leftMapsList[leftRnd].ObjTransform.position;
    //    rightTele.transform.position = rightMapsList[rightRnd].ObjTransform.position;
    //}
}

