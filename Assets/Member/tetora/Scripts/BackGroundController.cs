using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    Image[] _backGrounds;
    [SerializeField]
    float[] _backGroundSpeed;

    Image[] _backGroundSpriteOrigins;
    Image[] _backGroundSpriteClones;
    float _spriteSize;
    bool _isNextSprite;//true:Origin false:Clone

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
        _spriteSize = _backGrounds[0].sprite.bounds.size.x;
        _backGroundSpriteOrigins = new Image[_backGrounds.Length];
        _backGroundSpriteClones = new Image[_backGrounds.Length];
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
        switch (_isNextSprite)
        {
            case true:
                for (int i = 0; i < _backGroundSpriteOrigins.Length; i++)
                {
                    _backGroundSpriteOrigins[i].transform.position =
                        new Vector2(_backGroundSpriteClones[i].transform.position.x - _spriteSize,
                        _backGroundSpriteClones[i].transform.position.y);
                }
                _isNextSprite = false;
                break;
            case false:
                for (int i = 0; i < _backGroundSpriteClones.Length; i++)
                {
                    _backGroundSpriteClones[i].transform.position =
                        new Vector2(_backGroundSpriteOrigins[i].transform.position.x - _spriteSize,
                        _backGroundSpriteOrigins[i].transform.position.y);
                }
                _isNextSprite = true;
                break;
        }
    }
}
