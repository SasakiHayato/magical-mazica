using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    /// <summary>�摜</summary>
    private Sprite _sprite;
    /// <summary>��ѕ�</summary>
    private UseType _useType;
    /// <summary>�_���[�W</summary>
    private int _damage;

    /// <summary>
    /// �摜��p�����[�^�[�̐ݒ�
    /// </summary>
    public void Setup(Sprite sprite, UseType useType, int damage)
    {
        _sprite = sprite;
        _useType = useType;
        _damage = damage;
    }

    /// <summary>
    /// Bullet�̃C���X�^���X�𐶐����ĕԂ�
    /// </summary>
    public static Bullet Init(Bullet original, Sprite sprite, UseType useType, int damage)
    {
        Bullet ret = Instantiate(original);
        ret.Setup(sprite, useType, damage);
        return ret;
    }
}
