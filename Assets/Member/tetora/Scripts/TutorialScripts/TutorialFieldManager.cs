using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TutorialFieldManager : MonoBehaviour, IGameSetupable, IFieldEffectable
{
    [SerializeField]
    CharacterManager _characterManager;
    [SerializeField]
    Transform _createPlayerPos;
    [SerializeField]
    float _hitStopTime;

    Subject<List<RawMaterialID>> _materialIDSubject = new Subject<List<RawMaterialID>>();
    /// <summary>���̃X�e�[�W�Ŏg���f��ID��List�𔭍s����Subject</summary>
    public IObservable<List<RawMaterialID>> MaterialList => _materialIDSubject;

    int IGameSetupable.Priority => 3;
    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }

    void Start()
    {
        EffectStocker.Instance.AddFieldEffect(FieldEffect.EffectType.HitStop, this);
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
        //�v���C���[�������ɑf�ނ���������
        _characterManager.PlayerSpawn.Subscribe(p =>
        {
            //�Ƃ肠����100���炢�������Ƃ�
            defMaterials.ForEach(m =>
            {
                p.Storage.AddMaterial(m, 5);
            });
        })
        .AddTo(_characterManager);

        //�v���C���[�𐶐�
        _characterManager.CreatePlayer(_createPlayerPos);
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
}
