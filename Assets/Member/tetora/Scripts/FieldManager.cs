using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    //[SerializeField] Transform _playerSpawnPosition;
    [SerializeField] CreateMap _createMap;
    int _hierarchyNum;
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }
    private void Start()
    {
        Setup();
    }
    public void Setup()
    {
        _characterManager.Setup();
        _createMap.InitialSet();
        _characterManager.CreatePlayer(_createMap.PlayerTransform);
        //_characterManager.CreatePlayer(_playerSpawnPosition); �H����
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
