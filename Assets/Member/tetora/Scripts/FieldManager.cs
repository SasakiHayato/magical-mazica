using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] Transform _playerSpawnPosition;
    int _hierarchyNum;
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }

    public void Setup()
    {
        _characterManager.Setup();
        _characterManager.CreatePlayer(_playerSpawnPosition);
    }

    /// <summary>€–S”»’è</summary>
    /// <param name="type">€–S‚µ‚½ƒLƒƒƒ‰‚ÌType</param>
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
    /// <summary>GameOverˆ—</summary>
    void GameOver()
    {

    }
    /// <summary>GameClearˆ—</summary>
    void GameClear()
    {

    }
    /// <summary>Mob‚ª€‚ñ‚¾‚Æ‚«‚Ìˆ—</summary>
    void DeadMob()
    {

    }
}
public enum CharaType
{
    Player, Boss, Mob
}
