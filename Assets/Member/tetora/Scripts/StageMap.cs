using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMap : IEnumerable<Point>
{
    public int MaxX { get; }
    public int MaxY { get; }
    public int Length { get => _map.Length; }
    Point[] _map;
    public Point this[int id] => _map[id % MaxX + id / MaxX * MaxX];//インデクサー
    public Point this[int x, int y] => _map[x + y * MaxX];
    public Vector2 this[int id, float wallObjSize] => new Vector2(id % MaxX - MaxX / 2, id / MaxX - MaxY / 2) * wallObjSize;
    public int FloarCount
    {
        get
        {
            int count = 0;
            foreach (var floar in GetFloar())
            {
                count++;
            }
            return count;
        }
    }
    public StageMap(int maxX, int maxY)
    {
        this.MaxX = maxX;
        this.MaxY = maxY;
        _map = new Point[maxX * maxY];
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                _map[x + y * maxX] = new Point(x + y * maxX);
                _map[x + y * maxX].State = MapState.Wall;
                _map[x + y * maxX].IsGenerate = false;
            }
        }
    }
    /// <summary>床オブジェクトを検索する</summary>
    /// <returns>床オブジェクトのList</returns>
    public IEnumerable<Point> GetFloar()
    {
        foreach (var floar in _map)
        {
            if (floar.State == MapState.Floar)//Floarの場所を保存
            {
                yield return floar;
            }
        }
    }
    public int RandomFloarId()
    {
        int random = new System.Random().Next(0, this.FloarCount);
        foreach (var item in GetFloar())
        {
            random--;
            if (random <= 0)
            {
                return item.Id;
            }
        }
        return 0;
    }
    public IEnumerable<int> CheckDir(Point mapInfo, int releDis)
    {
        //右側を調べる
        if (mapInfo.Id % MaxX + releDis < MaxX)
        {
            yield return this[mapInfo.Id % MaxX + releDis, mapInfo.Id / MaxX].Id;
        }
        //左側を調べる
        if (mapInfo.Id % MaxX - releDis >= 0)
        {
            yield return this[mapInfo.Id % MaxX - releDis, mapInfo.Id / MaxX].Id;
        }
        //下側を調べる
        if (mapInfo.Id / MaxX - releDis >= 0)
        {
            yield return this[mapInfo.Id % MaxX, mapInfo.Id / MaxX - releDis].Id;
        }
        //上側を調べる
        if (mapInfo.Id / MaxX + releDis < MaxY)
        {
            yield return this[mapInfo.Id % MaxX, mapInfo.Id / MaxX + releDis].Id;
        }
    }

    public IEnumerator<Point> GetEnumerator()
    {
        int count = 0;
        while (count < _map.Length)
        {
            yield return _map[count];
            count++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()//foreachで回すために必要なインターフェース
    {
        yield return _map.GetEnumerator();
    }
}
public class Point
{
    MapState _state = MapState.Wall;
    public MapState State { get => _state; set => _state = value; }
    Transform _objTransform;
    public Transform ObjTransform { get => _objTransform; set => _objTransform = value; }
    bool _isGenerate;
    public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }
    public readonly int Id;
    public Point(int id)
    {
        Id = id;
    }

}
