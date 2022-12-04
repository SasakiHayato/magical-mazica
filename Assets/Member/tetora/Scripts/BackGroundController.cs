using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] _backGrounds;
    [SerializeField, Tooltip("Player�̑��x�ɂ�����W��")]
    float[] _backGroundSpeed;//(��)[0] = 1,[1] = 0.8, [2] = 0.5
    [SerializeField]
    float _spriteSize = 18;

    SpriteRenderer[] _backgroundSpriteClones;
    SpriteRenderer[] _nextBackGrounds;//���Ɉړ�����w�i
    private void Start()
    {
        InitialSetting();
    }
    private void Update()
    {

    }
    /// <summary>
    /// �w�i�̏����ݒ�
    /// </summary>
    void InitialSetting()
    {
        Debug.Log("�����ݒ�");
        _backgroundSpriteClones = new SpriteRenderer[_backGrounds.Length];
        CreateOrigin();
        CreateClone();

    }
    void CreateOrigin()
    {
        for (int i = 0; i < _backGrounds.Length; i++)
        {
            var ob = Instantiate(_backGrounds[i], transform);
            ob.transform.position = Vector2.zero;
        }
    }
    void CreateClone()
    {
        //�w�i�̎�ނ������ꂼ��̃N���[�����쐬���A�ʒu�����炵�Ă�
        for (int i = 0; i < _backGrounds.Length; i++)
        {
            _backgroundSpriteClones[i] = Instantiate(_backGrounds[i], transform);
            _backgroundSpriteClones[i].transform.position =
                new Vector2(_spriteSize, _backGrounds[i].transform.position.y);
        }
    }
    void SetBackGroundPos()
    {

    }
}
