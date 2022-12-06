using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCalcDistance : MonoBehaviour
{
    [SerializeField]
    float _checkDis = 20;

    GameObject _player;
    Transform _preTransform;
    private void Start()
    {
        GetPlayer();
        SetMeasure();
    }
    private void Update()
    {
        CalcDis();
    }
    void GetPlayer()
    {
        _player = FindObjectOfType<Player>().gameObject;
        _preTransform = _player.transform;
    }
    void SetMeasure()
    {
        transform.position = new Vector2(_preTransform.position.x - _checkDis, 0);
        _preTransform = gameObject.transform;
    }
    void CalcDis()
    {
        if (_player.transform.position.x - gameObject.transform.position.x < _checkDis)
        {
            Debug.Log("”wŒiˆÚ“®");
            BackGroundController.Instance.SetBackGroundPos();
            SetMeasure();
        }
    }
}
