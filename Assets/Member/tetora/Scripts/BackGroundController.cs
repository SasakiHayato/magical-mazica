using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] _backGrounds;
    [SerializeField, Tooltip("Playerの速度にかける係数")]
    float[] _backGroundSpeed;//(例)[0] = 1,[1] = 0.8, [2] = 0.5

    float _spriteSize;
    SpriteRenderer[] _backGroundSpriteOrigins;
    SpriteRenderer[] _backGroundSpriteClones;
    bool _nextSprite;//true:Origin false:Clone

    public static BackGroundController Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        InitialSetting();
    }
    /// <summary>
    /// 背景の初期設定
    /// </summary>
    void InitialSetting()
    {
        Debug.Log("初期設定");
        _spriteSize = _backGrounds[0].bounds.size.x;
        _backGroundSpriteOrigins = new SpriteRenderer[_backGrounds.Length];
        _backGroundSpriteClones = new SpriteRenderer[_backGrounds.Length];
        CreateOrigin();
        CreateClone();
    }
    void CreateOrigin()
    {
        for (int i = 0; i < _backGrounds.Length; i++)
        {
            _backGroundSpriteOrigins[i] = Instantiate(_backGrounds[i], gameObject.transform);
            _backGroundSpriteOrigins[i].transform.position = Vector2.zero;
        }
    }
    void CreateClone()
    {
        //背景の種類だけそれぞれのクローンを作成し、位置をずらしてる
        for (int i = 0; i < _backGrounds.Length; i++)
        {
            _backGroundSpriteClones[i] = Instantiate(_backGrounds[i], transform);
            _backGroundSpriteClones[i].transform.position =
                new Vector2(-_spriteSize, _backGrounds[i].transform.position.y);
        }
    }
    public void SetBackGroundPos()
    {
        switch (_nextSprite)
        {
            case true:
                for (int i = 0; i < _backGroundSpriteOrigins.Length; i++)
                {
                    _backGroundSpriteOrigins[i].transform.position =
                        new Vector2(_backGroundSpriteClones[i].transform.position.x - _spriteSize,
                        _backGroundSpriteClones[i].transform.position.y);
                }
                _nextSprite = false;
                break;
            case false:
                for (int i = 0; i < _backGroundSpriteClones.Length; i++)
                {
                    _backGroundSpriteClones[i].transform.position =
                        new Vector2(_backGroundSpriteOrigins[i].transform.position.x - _spriteSize,
                        _backGroundSpriteOrigins[i].transform.position.y);
                }
                _nextSprite = true;
                break;
        }
    }
}
