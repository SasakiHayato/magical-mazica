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
    /// ���������ăX�e�[�W�����
    /// </summary>
    void CheckCreate()
    {
        if (_player.transform.position.x - transform.position.x < _checkDis)
        {
            _createCount++;
            //�X�e�[�W������֐����Ă�
            CreateBossStage.Instance.CreateMap();            
            SetMeasureTool();
        }
    }
    /// <summary>
    /// �X�e�[�W������
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
    /// Player�̏����擾���Ă���
    /// </summary>
    void GetPlayer()
    {
        _player = FindObjectOfType<Player>().gameObject;
        _preTransform = _player.transform;
    }
    /// <summary>
    /// measure��z�u����
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
