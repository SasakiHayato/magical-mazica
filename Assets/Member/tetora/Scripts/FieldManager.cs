using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour, IGameSetupable
{
    [SerializeField] CharacterManager _characterManager;
    
    [SerializeField] CreateMap _createMap;
    int _hierarchyNum;
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }

    int IGameSetupable.Priority => 3;

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }

    public void Setup()
    {
        // _characterManager.Setup();
        // _createMap.InitialSet();
        // _characterManager.CreatePlayer(_createMap.PlayerTransform);
    }

    void IGameSetupable.GameSetup()
    {
        _characterManager.CreatePlayer(_createMap.PlayerTransform);
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
                //DeadMob();
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

    /// <summary>Mob‚ª€‚ñ‚¾‚Æ‚«‚Ìˆ—</summary> //Note. ‚à‚µ‚©‚µ‚½‚ç‚¢‚ç‚È‚¢
    //void DeadMob()
    //{

    //}
}
public enum CharaType
{
    Player, Boss, Mob
}
