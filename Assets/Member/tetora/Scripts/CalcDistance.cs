using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    [SerializeField]
    float _distance;

    GameObject _player;

    private void Start()
    {
        GetPlayer();
        SetMeasureTool();
    }
    private void Update()
    {
        if (_player.transform.position.x - transform.position.x < _distance)
        {
            //ステージを一つ作る関数を呼ぶ
            CreateBossStage.Instance.CreateMap();
            SetMeasureTool();
        }
    }
    void GetPlayer()
    {
        _player = FindObjectOfType<Player>().gameObject;
    }
    void SetMeasureTool()
    {
        gameObject.transform.position = new Vector2(_player.transform.position.x - _distance * 2, _player.transform.position.y);
    }
}
