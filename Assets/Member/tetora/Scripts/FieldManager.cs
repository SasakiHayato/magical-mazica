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
                //DeadMob();
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

    /// <summary>Mob�����񂾂Ƃ��̏���</summary> //Note. �����������炢��Ȃ�
    //void DeadMob()
    //{

    //}
}
public enum CharaType
{
    Player, Boss, Mob
}
