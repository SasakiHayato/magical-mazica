using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �Z���A�C�e���̃C���X�^���X�p�N���X
/// </summary>
public class FusionItem : MonoBehaviour
{
    private string _name;
    private Sprite _icon;
    private Sprite _sprite;
    private int _damage;
    private UseType _useType;

    public void Setup(FusionDatabase fusionDatabase, int damage)
    {
        _name = fusionDatabase.Name;
        _icon = fusionDatabase.Icon;
        _sprite = fusionDatabase.Sprite;
        _useType = fusionDatabase.UseType;
        _damage = damage;
    }

    /// <summary>
    /// �f�[�^�������������Ă��邩���m���߂�e�X�g�p�֐�
    /// </summary>
    public void CheckData()
    {
        Debug.Log($"Name;{_name} UseType:{_useType} Damage:{_damage}");
    }
}
