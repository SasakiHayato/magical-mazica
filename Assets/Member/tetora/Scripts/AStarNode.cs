using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : MonoBehaviour
{
    readonly Point _position;

    AStarNode _rootNode;

    StageMap _stageMap;

    /// <summary>���������v�̕���</summary>
    public int MoveTotalCost;

    /// <summary>�S�[���܂ł̕���</summary>
    private int _goalCost;
    /// <summary>�ړ��R�X�g</summary>
    public int MoveCost { get; }
    public int Score => MoveCost + _goalCost + MoveTotalCost;
    public AStarNode(int cost, Point position)
    {
        MoveCost = cost;
        _position = position;
    }
    public void GetStage(StageMap stage)
    {
        _stageMap = stage;
    }

    /// <summary>
    /// �S�[���܂ł̃R�X�g���v�Z
    /// </summary>
    public void SetEstimateCost(Point position, Point goal, bool isDiagonally)
    {
        var dx = Mathf.Abs((position.Id - goal.Id) % _stageMap.MaxX);
        var dy = Mathf.Abs((position.Id - goal.Id) / _stageMap.MaxY);
        _goalCost = isDiagonally ? Mathf.Max(dx, dy) : dx + dy;
    }

    public void Open(AStarNode rootNode)
    {
        _rootNode = rootNode;
        MoveTotalCost = 0;
        if (_rootNode == null)
            return;

        MoveTotalCost = _rootNode.MoveTotalCost + MoveCost;
    }

    public List<Point> ToList()
    {
        var list = new List<Point>();
        list.Insert(0, _position);

        var node = _rootNode;
        while (node != null)
        {
            list.Insert(0, node._position);
            node = node._rootNode;
        }

        return list;
    }
}
