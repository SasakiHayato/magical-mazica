using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] _backGrounds;
    [SerializeField, Tooltip("Player‚Ì‘¬“x‚É‚©‚¯‚éŒW”")]
    float[] _backGroundSpeed;//(—á)[0] = 1,[1] = 0.8, [2] = 0.5
    [SerializeField]
    float _spriteSize = 18;

    SpriteRenderer[] _backgroundSpriteClones;
    SpriteRenderer[] _nextBackGrounds;//Ÿ‚ÉˆÚ“®‚·‚é”wŒi
    private void Start()
    {
        InitialSetting();
    }
    private void Update()
    {

    }
    /// <summary>
    /// ”wŒi‚Ì‰Šúİ’è
    /// </summary>
    void InitialSetting()
    {
        Debug.Log("‰Šúİ’è");
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
        //”wŒi‚Ìí—Ş‚¾‚¯‚»‚ê‚¼‚ê‚ÌƒNƒ[ƒ“‚ğì¬‚µAˆÊ’u‚ğ‚¸‚ç‚µ‚Ä‚é
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
