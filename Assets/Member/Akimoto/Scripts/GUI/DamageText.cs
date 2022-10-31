using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �_���[�W���󂯂����̃e�L�X�g
/// </summary>
public class DamageText : MonoBehaviour
{
    [SerializeField] Text _text;
    /// <summary>�e�L�X�g�̈ړ�����</summary>
    [SerializeField] Vector2 _moveDistance;
    /// <summary>�e�L�X�g���\�������܂ł̎���</summary>
    [SerializeField] float _fadeinDuration;
    /// <summary>�e�L�X�g��������܂ł̎���</summary>
    [SerializeField] float _fadeoutDuration;
    /// <summary>�\������</summary>
    [SerializeField] float _duration;
    private Sequence _sequence;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="original">�������̃I�u�W�F�N�g</param>
    /// <param name="value">�\��������������</param>
    /// <param name="position">�����ʒu</param>
    /// <param name="color">�e�L�X�g�̐F</param>
    /// <param name="ease">�ݒ肷��C�[�W���O (�e�C�[�W���O�̓����ɂ��Ă͂�������Q�l�ɂ��Ă������� https://game-ui.net/?p=835 )</param>
    /// <returns></returns>
    public static DamageText Init(DamageText original, string value, Vector2 position, Color color, Ease ease = Ease.OutQuad)
    {
        DamageText ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(value, color);
        ret.transform.position = position;
        ret.Animation(ease);
        return ret;
    }

    public static DamageText Init(DamageText original, string value, Vector2 position, Color color, float fadeinDuration, float fadeoutDuration, Vector2 moveDistance, float moveDuration, Ease ease = Ease.OutQuad)
    {
        DamageText ret = Instantiate(original, position, Quaternion.identity);
        ret.Setup(value, color);
        ret.transform.position = position;
        ret.Animation(ease, fadeinDuration, fadeoutDuration, moveDuration, moveDistance.x, moveDistance.y, true);
        return ret;
    }

    private void Setup(string value, Color color)
    {
        _text.text = value;
        _text.color = color;
    }

    private void Animation(Ease ease, float fadeinDuration = 0, float fadeoutDuration = 0, float moveDistanceX = 0, float moveDistanceY = 0, float moveDuration = 0, bool isCustom = false)
    {
        Color c = _text.color;
        _text.color = Color.clear;
        _sequence = DOTween.Sequence();
        if (isCustom)
        {
            _sequence
            .SetEase(ease)
            .Append(_text.DOColor(c, fadeinDuration))
            .Join(transform.DOMove(new Vector2(transform.position.x + moveDistanceX, transform.position.y + moveDistanceY), moveDuration))
            .Append(_text.DOColor(Color.clear, fadeoutDuration))
            .OnComplete(() => Destroy(gameObject));
        }
        else
        {
            _sequence
            .SetEase(ease)
            .Append(_text.DOColor(c, _fadeinDuration))
            .Join(transform.DOMove(new Vector2(transform.position.x + _moveDistance.x, transform.position.y + _moveDistance.y), _duration))
            .Append(_text.DOColor(Color.clear, _fadeoutDuration))
            .OnComplete(() => Destroy(gameObject));
        }
    }

    public void Kill()
    {
        _sequence.Kill();
    }
}
