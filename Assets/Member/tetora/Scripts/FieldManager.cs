using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    int _hierarchyNum;
    public static FieldManager Instance { get; private set; }
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
    private void Start()
    {

    }
    /// <summary>死亡判定</summary>
    /// <param name="type">死亡したキャラのType</param>
    void OnGameEndJudge(CharaType type)
    {
        switch (type)
        {
            case CharaType.Player:
                GameOver();
                break;
            case CharaType.Boss:
                GameClear();
                break;
            case CharaType.Mob:
                DeadMob();
                break;
        }
    }
    /// <summary>GameOver処理</summary>
    void GameOver()
    {

    }
    /// <summary>GameClear処理</summary>
    void GameClear()
    {

    }
    /// <summary>Mobが死んだときの処理</summary>
    void DeadMob()
    {

    }
}
public enum CharaType
{
    Player, Boss, Mob
}
