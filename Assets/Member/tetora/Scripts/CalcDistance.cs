using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    [SerializeField]
    float _checkDis = 18;

    int _createCount;
    Transform _preTransform;
    GameObject _player;

    private void Start()
    {
        GetPlayer();
        SetMeasureTool();
    }
    private void Update()
    {
        CheckCreate();
        CheckDestroy();
    }
    /// <summary>
    /// 距離測ってステージを作る
    /// </summary>
    void CheckCreate()
    {
        if (_player.transform.position.x - transform.position.x < _checkDis)
        {
            _createCount++;
            //ステージを一つ作る関数を呼ぶ
            CreateBossStage.Instance.CreateMap();            
            SetMeasureTool();
        }
    }
    /// <summary>
    /// ステージを消す
    /// </summary>
    void CheckDestroy()
    {
        if (_createCount > 1)
        {
            _createCount = 0;
            CreateBossStage.Instance.DestroyMap();
        }
    }
    /// <summary>
    /// Playerの情報を取得してくる
    /// </summary>
    void GetPlayer()
    {
        _player = FindObjectOfType<Player>().gameObject;
        _preTransform = _player.transform;
    }
    /// <summary>
    /// measureを配置する
    /// </summary>
    void SetMeasureTool()
    {
        gameObject.transform.position
            = new Vector2(_preTransform.position.x - _checkDis * CreateBossStage.Instance.CreateCount()
            , _player.transform.position.y);
        _preTransform = gameObject.transform;
        CreateBossStage.Instance.CreatedNum = 0;
    }
}
