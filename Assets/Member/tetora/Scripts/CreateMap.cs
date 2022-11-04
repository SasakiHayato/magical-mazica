using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    float _wallObjSize = 3;//マップ一つ一つのサイズ
    Map[,] _map;
    int _startDigPos;//掘り始める始点
    /// <summary>初期設定</summary>
    public void InitialSet()
    {
        //壁オブジェクトのScaleSizeを入れる
        _wallObjSize = _wallObj.transform.localScale.x;
        SetWall();
        StartDig();
    }
    /// <summary>全てのマスを壁にする</summary>
    void SetWall()
    {
        _map = new Map[_scriptableObject.MapHorSide, _scriptableObject.MapVerSide];
        for (int y = 0; y < _scriptableObject.MapVerSide; y++)
        {
            for (int x = 0; x < _scriptableObject.MapHorSide; x++)
            {
                _map[x, y] = new Map(x + y * _scriptableObject.MapHorSide);
                _map[x, y].State = MapState.Wall;
            }
        }
    }
    /// <summary>ランダムな開始地点を決める</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_scriptableObject.MapHorSide - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_scriptableObject.MapVerSide - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _scriptableObject.MapHorSide;
    }
    /// <summary>4方向をランダムな順番で返す</summary>
    /// <param name="id">開始地点のid</param>
    /// <returns>4方向のid</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_scriptableObject.MapHorSide * 2),id - _scriptableObject.MapHorSide), //上
            (id + _scriptableObject.MapHorSide * 2,id + _scriptableObject.MapHorSide), //下
            (id + 2,id + 1), //右
            (id - 2,id - 1) //左
        };
        for (int i = 0; i < twoTargetDirs.Length; i++)
        {
            //順番をシャッフルする
            int r = UnityEngine.Random.Range(0, twoTargetDirs.Length);
            var tmp = twoTargetDirs[i];
            twoTargetDirs[i] = twoTargetDirs[r];
            twoTargetDirs[r] = tmp;
        }
        foreach ((int two, int one) dir in twoTargetDirs)
        {
            //two:2つ先のid ,one:1つ先のid
            if (dir.two < 0 || dir.two > _map.Length - 1)//範囲内のidのみ値を返す
            {
                continue;
            }
            //横のサイズを超えないか
            if (id % _scriptableObject.MapHorSide < 1 || id % _scriptableObject.MapHorSide >= _scriptableObject.MapHorSide - 1)
            {
                continue;
            }
            //2個先が壁か確認
            if (_map[dir.two % _scriptableObject.MapHorSide, dir.two / _scriptableObject.MapHorSide].State != MapState.Wall)
            {
                continue;
            }
            //1個先が壁か確認
            if (_map[dir.one % _scriptableObject.MapHorSide, dir.one / _scriptableObject.MapHorSide].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _scriptableObject.MapHorSide == 0 || dir.two / _scriptableObject.MapHorSide == _scriptableObject.MapVerSide - 1)
            {
                continue;
            }
            if (dir.two % _scriptableObject.MapHorSide == 0 || dir.two % _scriptableObject.MapHorSide == _scriptableObject.MapHorSide - 1)
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
        _map[id % _scriptableObject.MapHorSide, id / _scriptableObject.MapHorSide].State = MapState.Floar;
        foreach (var posId in CheckDir(id))
        {
            _map[posId.oneTarget % _scriptableObject.MapHorSide, posId.oneTarget / _scriptableObject.MapHorSide].State = MapState.Floar;
            Dig(posId.twoTarget);
        }
    }
    /// <summary>掘り始める</summary>
    void StartDig()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _map)
        {
            if (pos.State == MapState.Floar)
            {
                var emptyObj = new GameObject();//空のオブジェクトを生成
                emptyObj.transform.position = new Vector2(pos.Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                    pos.Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
                pos.ObjTransform = emptyObj.transform;
                continue;
            }
            else
            {
                var wall = Instantiate(_wallObj);
                wall.transform.SetParent(_parentObj.transform);
                wall.transform.position = new Vector2(pos.Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                    pos.Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
                pos.ObjTransform = wall.transform;
            }
        }
    }
    /// <summary>敵の生成</summary>
    void CreateEnemy()
    {
        for (int i = 0; i < _scriptableObject.EnemyCount; i++)
        {
            //Enemyの出現
            int enemyRnd = new System.Random().Next(0, _scriptableObject.EnemyObject.Length);//敵の種類
            GameObject enemy = Instantiate(_scriptableObject.EnemyObject[enemyRnd]);
            int random = new System.Random().Next(0, GetFloar().Count);//床のランダムな場所を決める
            enemy.transform.position = new Vector2(GetFloar()[random].Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                GetFloar()[random].Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
            GetFloar().RemoveAt(random);
        }
    }
    /// <summary>床オブジェクトのListを返す</summary>
    /// <returns>床オブジェクトのList</returns>
    List<Map> GetFloar()
    {
        List<Map> generatablePosList = new List<Map>();//床の数
        foreach (var floar in _map)
        {
            if (floar.State == MapState.Floar)//Floarの場所を保存
            {
                generatablePosList.Add(floar);
            }
        }
        return generatablePosList;
    }
    /// <summary>Player生成の場所を決める</summary>
    public Transform DecisionPlayerPos()
    {
        foreach (Map map in _map)
        {
            print(map.ObjTransform);
        }
        int rndId = new System.Random().Next(0, GetFloar().Count);//床オブジェクトの入っているListの要素数からランダムな値を取得
        Map rndMap = GetFloar()[rndId];//床オブジェクトのランダムなオブジェクトを取得
        foreach (var item in _map)
        {
            if (item.Id + _scriptableObject.MapHorSide == rndMap.Id)
            {
                if (item.State == MapState.Wall)
                {
                    return rndMap.ObjTransform;
                }
            }
        }
        Debug.Log($"rndId:{rndId},一個下のId{_map[(rndId + _scriptableObject.MapHorSide) % _scriptableObject.MapHorSide, (rndId + _scriptableObject.MapHorSide) / _scriptableObject.MapHorSide].Id}");
        return DecisionPlayerPos();
    }
}
class Map
{
    MapState _state = MapState.Wall;
    public MapState State { get => _state; set => _state = value; }
    Transform _objTransform;
    public Transform ObjTransform { get => _objTransform; set => _objTransform = value; }
    public readonly int Id;
    public Map(int id)
    {
        Id = id;
    }
}
