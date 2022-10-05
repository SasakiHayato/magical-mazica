using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MapState
{
    Wall, Floar
}
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    int _mapVerSide = 15;//縦の長さ
    [SerializeField]
    int _mapHorSide = 31;//横の長さ
    [SerializeField]
    GameObject _wallObj;
    Map[,] _map;
    int _startDigPos;//掘り始める始点
    private void Start()
    {
        SetWall();
        StartDig();
    }
    /// <summary>全てのマスを壁にする</summary>
    void SetWall()
    {
        _map = new Map[_mapHorSide, _mapVerSide];
        for (int y = 0; y < _mapVerSide; y++)
        {
            for (int x = 0; x < _mapHorSide; x++)
            {
                _map[x, y] = new Map(x + y * _mapHorSide);
                _map[x, y].State = MapState.Wall;
            }
        }
    }
    /// <summary>ランダムな開始地点を決める</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_mapHorSide - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_mapVerSide - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _mapHorSide;
    }
    /// <summary>4方向をランダムな順番で返す</summary>
    /// <param name="id">開始地点のid</param>
    /// <returns>4方向のid</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_mapHorSide * 2),id - _mapHorSide), //上
            (id + _mapHorSide * 2,id + _mapHorSide), //下
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
            if (id % _mapHorSide < 1 || id % _mapHorSide >= _mapHorSide - 1)
            {
                continue;
            }
            //2個先が壁か確認
            if (_map[dir.two % _mapHorSide, dir.two / _mapHorSide].State != MapState.Wall)
            {
                continue;
            }
            //1個先が壁か確認
            if (_map[dir.one % _mapHorSide, dir.one / _mapHorSide].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _mapHorSide == 0 || dir.two / _mapHorSide == _mapVerSide - 1)
            {
                continue;
            }
            if (dir.two % _mapHorSide == 0 || dir.two % _mapHorSide == _mapHorSide - 1)
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
        _map[id % _mapHorSide, id / _mapHorSide].State = MapState.Floar;
        foreach (var posId in CheckDir(id))
        {
            _map[posId.oneTarget % _mapHorSide, posId.oneTarget / _mapHorSide].State = MapState.Floar;
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
                continue;
            }
            var wall = Instantiate(_wallObj);
            wall.transform.position = new Vector2(pos.Id % _mapHorSide, pos.Id / _mapHorSide);
        }
    }
}
class Map
{
    MapState _state = MapState.Wall;
    public MapState State { get => _state; set => _state = value; }
    public readonly int Id;
    public Map(int id)
    {
        Id = id;
    }
}
