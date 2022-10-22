using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static ExecuteData;

public class ExecuteProsseser
{
    List<DataBase> _dataBaseList;

    DataBase _dataBase;

    public Vector2 MoveDir { get; private set; }

    public ExecuteProsseser(ExecuteData data)
    {
        _dataBaseList = new List<DataBase>();

        _dataBaseList.Add(data.IdleData);
        _dataBaseList.Add(data.MoveData);
        _dataBaseList.Add(data.AttackData);

        _dataBaseList = _dataBaseList.OrderBy(d => d.Priority).ToList();
    }

    public Action OnNext(Transform user)
    {
        if (_dataBase != null)
        {
            InitalizeExecuteData();
        }

        return SetExecuteData(user);
    }

    Action SetExecuteData(Transform user)
    {
        Vector2 player = TestSetPlayer.Instance.Player.transform.position;

        try
        {
            _dataBase = _dataBaseList
            .First(d => d.AttributeDist > Vector2.Distance(user.position, player));
        }
        catch
        {
            _dataBase = _dataBaseList.Find(d => d.Path == "Idle");
        }

        Action action = null;

        switch (_dataBase.Path)
        {
            case "Idle":
                OnIdleData idle = _dataBase as OnIdleData;
                idle.ExecuteIdle.OnEnable();

                action = () => OnIdle(idle);
                break;

            case "Move":
                OnMoveData move = _dataBase as OnMoveData;
                move.ExecuteMove.OnEnable();

                action = () => OnMove(move);
                break;

            case "Attack":
                OnAttackData attack = _dataBase as OnAttackData;
                attack.ExecuteAttack.OnEnable();

                action = () => OnAttack(attack);
                break;
        }

        return action;
    }

    void InitalizeExecuteData()
    {
        switch (_dataBase.Path)
        {
            case "Idle":
                OnIdleData idle = _dataBase as OnIdleData;
                idle.ExecuteIdle.Initalize();

                break;

            case "Move":
                OnMoveData move = _dataBase as OnMoveData;
                move.ExecuteMove.Initalize();

                break;

            case "Attack":
                OnAttackData attack = _dataBase as OnAttackData;
                attack.ExecuteAttack.Initalize();

                break;
        }
    }

    void OnIdle(OnIdleData data)
    {
        MoveDir = data.ExecuteIdle.Execute();
    }

    void OnMove(OnMoveData data)
    {
        MoveDir = data.ExecuteMove.Execute();
    }

    void OnAttack(OnAttackData data)
    {
        MoveDir = data.ExecuteAttack.Execute();
    }
}
