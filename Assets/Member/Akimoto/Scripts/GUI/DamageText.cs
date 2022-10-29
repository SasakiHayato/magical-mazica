using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// ダメージを受けた時のテキスト
/// </summary>
public class DamageText : MonoBehaviour
{
    [SerializeField] TextMesh _textMesh;
    /// <summary>テキストの移動距離</summary>
    [SerializeField] Vector2 _moveDistance;
    /// <summary>表示時間</summary>
    [SerializeField] float _duration;
    private Sequence _sequence;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="original">生成元のオブジェクト</param>
    /// <param name="position">生成位置</param>
    /// <param name="color">テキストの色</param>
    /// <param name="ease">設定するイージング (各イージングの動きについてはこちらを参考にしてください https://game-ui.net/?p=835 )</param>
    /// <returns></returns>
    public static DamageText Init(DamageText original, Vector2 position, Color color, Ease ease = Ease.OutQuad)
    {
        DamageText ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(color);
        ret.transform.position = position;
        ret.Animation(ease);
        return ret;
    }

    private void Setup(Color color)
    {
        _textMesh.color = color;
    }

    private void Animation(Ease ease)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .SetEase(ease)
            .Append(_textMesh.transform.DOMove(new Vector2(transform.position.x + _moveDistance.x, transform.position.y + _moveDistance.y), _duration))
            .OnComplete(() => Destroy(gameObject));
    }
}
