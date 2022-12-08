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

    public void Setup()
    {
        // _characterManager.Setup();
        // _createMap.InitialSet();
        // _characterManager.CreatePlayer(_createMap.PlayerTransform);
    }

    void IGameSetupable.GameSetup()
    {
        //今ステージで登場する素材たち
        //ステージデータを作る際はここも変更すること
        List<RawMaterialID> defMaterials = new List<RawMaterialID>()
        {
            RawMaterialID.BombBean,
            RawMaterialID.PowerPlant,
            RawMaterialID.Penetration,
            RawMaterialID.Poison
        };
        _materialIDSubject.OnNext(defMaterials);
        _characterManager.PlayerSpawn.Subscribe(p =>
        {
            //とりあえず100個くらい
            defMaterials.ForEach(m =>
            {
                p.Storage.AddMaterial(m, 100);
            });
        })
        .AddTo(_characterManager);

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

    /// <summary>Mobが死んだときの処理</summary> //Note. もしかしたらいらない
    //void DeadMob()
    //{

    //}
}
public enum CharaType
{
    Player, Boss, Mob
}
