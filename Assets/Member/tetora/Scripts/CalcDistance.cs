using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    [SerializeField]
    float _distance;

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
    void CheckCreate()
    {
        if (_player.transform.position.x - transform.position.x < _distance)
        {
            //ステージを一つ作る関数を呼ぶ
            CreateBossStage.Instance.CreateMap();
            _createCount++;
            SetMeasureTool();
        }
    }
    void CheckDestroy()
    {
        if (_createCount >= 2)
        {
            _createCount = 0;
            CreateBossStage.Instance.DestroyMap();
        }
    }
    void GetPlayer()
    {
        _player = FindObjectOfType<Player>().gameObject;
        _preTransform = _player.transform;
    }
    void SetMeasureTool()
    {
        gameObject.transform.position
            = new Vector2(_preTransform.position.x - _distance * CreateBossStage.Instance.CreateCount()
            , _player.transform.position.y);
        _preTransform = gameObject.transform;
        CreateBossStage.Instance.CreatedNum = 0;
    }
}
