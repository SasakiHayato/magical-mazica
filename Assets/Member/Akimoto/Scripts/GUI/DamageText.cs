using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// ダメージを受けた時のテキスト
/// </summary>
public class DamageText : MonoBehaviour
{
    [SerializeField] Text _text;
    /// <summary>テキストの移動距離</summary>
    [SerializeField] Vector2 _moveDistance;
    /// <summary>テキストが表示されるまでの時間</summary>
    [SerializeField] float _fadeinDuration;
    /// <summary>テキストが消えるまでの時間</summary>
    [SerializeField] float _fadeoutDuraion;
    /// <summary>表示時間</summary>
    [SerializeField] float _duration;
    private Sequence _sequence;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="original">生成元のオブジェクト</param>
    /// <param name="value">表示したい文字列</param>
    /// <param name="position">生成位置</param>
    /// <param name="color">テキストの色</param>
    /// <param name="ease">設定するイージング (各イージングの動きについてはこちらを参考にしてください https://game-ui.net/?p=835 )</param>
    /// <returns></returns>
    public static DamageText Init(DamageText original, string value ,Vector2 position, Color color, Ease ease = Ease.OutQuad)
    {
        DamageText ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(value, color);
        ret.transform.position = position;
        ret.Animation(ease);
        return ret;
    }

    private void Setup(string value, Color color)
    {
        _text.text = value;
        _text.color = color;
    }

    private void Animation(Ease ease)
    {
        Color c = _text.color;
        _text.color = Color.clear;
        _sequence = DOTween.Sequence();
        _sequence
            .SetEase(ease)
            .Append(_text.DOColor(c, _fadeinDuration))
            .Join(transform.DOMove(new Vector2(transform.position.x + _moveDistance.x, transform.position.y + _moveDistance.y), _duration))
            .Append(_text.DOColor(Color.clear, _fadeoutDuraion))
            .OnComplete(() => Destroy(gameObject));
    }
}
