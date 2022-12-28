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
    GameObject _enemy;
    [SerializeField]
    Transform _createPlayerPos;
    [SerializeField]
    Transform _createEnemyPos;
    [SerializeField]
    Transform _enemyParent;
    [SerializeField]
    float _hitStopTime;

    Subject<List<RawMaterialID>> _materialIDSubject = new Subject<List<RawMaterialID>>();
    /// <summary>このステージで使う素材IDのListを発行するSubject</summary>
    public IObservable<List<RawMaterialID>> MaterialList => _materialIDSubject;

    int IGameSetupable.Priority => 3;

    public static TutorialFieldManager Instance { get; private set; }
    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
        Instance = this;
    }

    void Start()
    {
        EffectStocker.Instance.AddFieldEffect(FieldEffect.EffectType.HitStop, this);
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
        //プレイヤー生成時に素材を持たせる
        _characterManager.PlayerSpawn.Subscribe(p =>
        {
            //とりあえず100個くらい持たせとく
            defMaterials.ForEach(m =>
            {
                p.Storage.AddMaterial(m, 5);
            });
        })
        .AddTo(_characterManager);

        //プレイヤーを生成
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
    public void CreateEnemy()
    {
        GameObject enemy = Instantiate(_enemy, _enemyParent);
        enemy.transform.position = _createEnemyPos.position;
    }
}
