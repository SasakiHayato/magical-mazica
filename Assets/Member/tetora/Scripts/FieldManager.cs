using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class FieldManager : MonoBehaviour, IGameSetupable, IFieldEffectable
{
    [SerializeField] float _hitStopTime;
    [SerializeField] CharacterManager _characterManager;
    
    [SerializeField] MapCreaterBase _createMap;
    int _hierarchyNum;
    Subject<List<RawMaterialID>> _materialIDSubject = new Subject<List<RawMaterialID>>();
    public int HierarchyNum { get => _hierarchyNum; set => _hierarchyNum = value; }
    public IObservable<List<RawMaterialID>> MaterialList => _materialIDSubject;

    int IGameSetupable.Priority => 3;

    void Awake()
    {
        EffectStocker.Instance.AddFieldEffect(FieldEffect.EffectType.HitStop, this);
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

    void IFieldEffectable.Execute()
    {
        StartCoroutine(OnHitStop());
    }

    IEnumerator OnHitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(_hitStopTime);
        Time.timeScale = 1;
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
