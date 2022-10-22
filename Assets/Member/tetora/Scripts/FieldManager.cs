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
    /// <summary>���S����</summary>
    /// <param name="type">���S�����L������Type</param>
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
    /// <summary>GameOver����</summary>
    void GameOver()
    {

    }
    /// <summary>GameClear����</summary>
    void GameClear()
    {

    }
    /// <summary>Mob�����񂾂Ƃ��̏���</summary>
    void DeadMob()
    {

    }
}
public enum CharaType
{
    Player, Boss, Mob
}
