using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    [SerializeField]
    float _distance;

    GameObject _player;

    void GetPlayer()
    {
        _player = GetComponent<Player>().gameObject;
    }
    void SetMeasureTool()
    {
        transform.position = new Vector2(_player.transform.position.x - _distance * 2, _player.transform.position.y);
    }
    void CalcDis()
    {
        if (_player.transform.position.x - transform.position.x >= _distance)
        {
            //�X�e�[�W������֐����Ă�
        }
    }
}
