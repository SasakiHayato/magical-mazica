using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class FieldManager : MonoBehaviour, IGameSetupable
{
    [SerializeField] CharacterManager _characterManager;
    
    [SerializeField] CreateMap _createMap;
    int _hierarchyNum;
    Subject<List<RawMaterialID>> _materialIDSubject = new Subject<List<RawMaterialID>>();
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }
    public IObservable<List<RawMaterialID>> MaterialList => _materialIDSubject;

    int IGameSetupable.Priority => 3;

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }
    private void Start()
    {
        _materialIDSubject.OnNext(new List<RawMaterialID>() 
        { RawMaterialID.BombBean
        ,RawMaterialID.PowerPlant
        ,RawMaterialID.Penetration
        ,RawMaterialID.Poison
        });
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
                //DeadMob();
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

    /// <summary>Mobが死んだときの処理</summary> //Note. もしかしたらいらない
    //void DeadMob()
    //{

    //}
}
public enum CharaType
{
    Player, Boss, Mob
}
