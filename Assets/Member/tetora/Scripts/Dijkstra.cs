using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Dijkstra
{
    static HashSet<Point> _mapCostList = new HashSet<Point>();//�R�X�g�������Ă�Point�̃��X�g
    static StageMap _stageData;
    static Point _startPoint;
    /// <summary>
    /// �R�X�g�̈�ԑ傫��Point��Ԃ�
    /// </summary>
    /// <returns>�R�X�g�̈�ԑ傫��Point</returns>
    public static Point GetGoalPoint(StageMap stage)
    {
        _stageData = stage;
        GetStartPoint();//start�ʒu��Player�̏ꏊ������
        AddAroundPoint(_startPoint);
        return _mapCostList.OrderByDescending(p => p.Cost).First();//�R�X�g�̈�ԑ傫��Point
    }

    /// <summary>
    /// ���߂̈ʒu��Player�̈ʒu�ɂ���
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
    /// ����̃}�X�ɃR�X�g+1
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
    /// �J����ꏊ�����邩�ǂ���
    /// </summary>
    /// <returns>true:�J����ꏊ������ false:�J����ꏊ���Ȃ�</returns>
    static bool CheckCanOpenPoint()
    {
        foreach (var item in _stageData.GetFloar())
        {
            if (item.IsOpen != true)//�܂��J����Ƃ��낪����Ȃ�
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// List�̒��ŃR�X�g�̈�ԏ������ꏊ����n�߂�
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
    /// 4�������ׂĊJ���ĂȂ����̃��X�g��Ԃ�
    /// </summary>
    static HashSet<Point> CheckAround(Point point)
    {
        HashSet<Point> returnList = new HashSet<Point>();
        if (_stageData[point.Id + _stageData.MaxX].State == MapState.Floar)//��
        {
            if (_stageData[point.Id + _stageData.MaxX].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id + _stageData.MaxX]);
            }
        }
        if (_stageData[point.Id - _stageData.MaxX].State == MapState.Floar)//��
        {
            if (_stageData[point.Id - _stageData.MaxX].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id - _stageData.MaxX]);
            }
        }
        if (_stageData[point.Id + 1].State == MapState.Floar)//�E
        {
            if (_stageData[point.Id + 1].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id + 1]);
            }
        }
        if (_stageData[point.Id - 1].State == MapState.Floar)//��
        {
            if (_stageData[point.Id - 1].IsOpen != true)
            {
                returnList.Add(_stageData[point.Id - 1]);
            }
        }
        return returnList;
    }
}
