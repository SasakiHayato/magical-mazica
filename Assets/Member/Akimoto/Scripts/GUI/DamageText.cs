using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �_���[�W���󂯂����̃e�L�X�g
/// </summary>
public class DamageText : MonoBehaviour
{
    [SerializeField] TextMesh _textMesh;
    /// <summary>�e�L�X�g�̈ړ�����</summary>
    [SerializeField] Vector2 _moveDistance;
    /// <summary>�\������</summary>
    [SerializeField] float _duration;
    private Sequence _sequence;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="original">�������̃I�u�W�F�N�g</param>
    /// <param name="position">�����ʒu</param>
    /// <param name="color">�e�L�X�g�̐F</param>
    /// <param name="ease">�ݒ肷��C�[�W���O (�e�C�[�W���O�̓����ɂ��Ă͂�������Q�l�ɂ��Ă������� https://game-ui.net/?p=835 )</param>
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
