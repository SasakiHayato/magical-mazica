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
        //���X�e�[�W�œo�ꂷ��f�ނ���
        //�X�e�[�W�f�[�^�����ۂ͂������ύX���邱��
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
            //�Ƃ肠����100���炢
            defMaterials.ForEach(m =>
            {
                p.Storage.AddMaterial(m, 100);
            });
        })
        .AddTo(_characterManager);

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

    /// <summary>Mob�����񂾂Ƃ��̏���</summary> //Note. �����������炢��Ȃ�
    //void DeadMob()
    //{

    //}
}
public enum CharaType
{
    Player, Boss, Mob
}
