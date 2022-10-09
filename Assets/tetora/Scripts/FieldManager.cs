using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }
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
                break;
        }
    }
    void GameOver()
    {

    }
    void GameClear()
    {

    }
}
public enum CharaType
{
    Player, Boss, Mob
}
