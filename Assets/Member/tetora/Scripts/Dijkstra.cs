using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Dijkstra
{
    static HashSet<Point> _mapCostList = new HashSet<Point>();//コストを持ってるPointのリスト
    static StageMap _stageData;
    static Point _startPoint;
    /// <summary>
    /// コストの一番大きいPointを返す
    /// </summary>
    /// <returns>コストの一番大きいPoint</returns>
    public static Point GetGoalPoint(StageMap stage)
    {
        _stageData = stage;
        GetStartPoint();//start位置にPlayerの場所を入れる
        AddAroundPoint(_startPoint);
        return _mapCostList.OrderByDescending(p => p.Cost).First();//コストの一番大きいPoint
    }

    /// <summary>
    /// 初めの位置をPlayerの位置にする
    /// </summary>
    static void GetStartPoint()
    {
        foreach (var item in _stageData)
        {
            if (item.State == MapState.Player)
            {
                _startPoint = item;
                _startPoint.Cost = 0;
                _startPoint.IsOpen = true;
                break;
            }
        }
    }
    /// <summary>
    /// 周りのマスにコスト+1
    /// </summary>
    /// <param name="point"></param>
    static void AddAroundPoint(Point point)
    {
        foreach (var item in CheckAround(point))
        {
            item.Cost += 1;
            _mapCostList.Add(item);
        }
        if (CheckCanOpenPoint())
        {
            AddAroundPoint(NextPoint());
        }
    }
    /// <summary>
    /// 開ける場所があるかどうか
    /// </summary>
    /// <returns>true:開ける場所がある false:開ける場所がない</returns>
    static bool CheckCanOpenPoint()
    {
        foreach (var item in _stageData.GetFloar())
        {
            if (item.IsOpen != true)//まだ開けるところがあるなら
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Listの中でコストの一番小さい場所から始める
    /// </summary>
    static Point NextPoint()
    {
        Point nextPoint = _mapCostList.OrderBy(p => p.Cost).First();
        if (nextPoint.IsOpen == true)
        {
            _mapCostList.Remove(nextPoint);
            return NextPoint();
        }
        else
        {
            nextPoint.IsOpen = true;
            return nextPoint;
        }
    }
    /// <summary>
    /// 4方向調べて開いてない床のリストを返す
    /// </summary>
    static HashSet<Point> CheckAround(Point point)
    {
        HashSet<Point> returnList = new HashSet<Point>();
        if (_stageData[point.Id + _stageData.MaxX].State == MapState.Floar)//上
        {
            if (_stageData[point.Id + _stageData.MaxX].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id + _stageData.MaxX]);
            }
        }
        if (_stageData[point.Id - _stageData.MaxX].State == MapState.Floar)//下
        {
            if (_stageData[point.Id - _stageData.MaxX].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id - _stageData.MaxX]);
            }
        }
        if (_stageData[point.Id + 1].State == MapState.Floar)//右
        {
            if (_stageData[point.Id + 1].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id + 1]);
            }
        }
        if (_stageData[point.Id - 1].State == MapState.Floar)//左
        {
            if (_stageData[point.Id - 1].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id - 1]);
            }
        }
        return returnList;
    }
}
