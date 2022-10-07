using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �Z���A�C�e���̊Ǘ��N���X
/// </summary>
public class FusionItem : MonoBehaviour
{
    [SerializeField] FusionData _fusionData;
    [SerializeField] Bullet _bulletPrefab;
    /// <summary>�A�C�e����</summary>
    private string _name;
    /// <summary>�A�C�R��</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>���������̉摜</summary>
    private Sprite _sprite;
    /// <summary>�_���[�W</summary>
    private int _damage;
    /// <summary>�g�p�����ۂ̋���</summary>
    private UseType _useType;
    /// <summary>�A�C�R���摜�̍X�V��ʒm����</summary>
    public System.IObservable<Sprite> ChangeIconObservable => _icon;

    public void Setup()
    {
        
    }

    /// <summary>
    /// �Z���J�n<br/>�n���ꂽ�f�ނ���Z����̃f�[�^�𒊏o����
    /// </summary>
    public void Fusion(RawMaterialID materialID1, RawMaterialID materialID2)
    {
        var data = _fusionData.GetFusionData(materialID1, materialID2, ref _damage);

        _name = data.Name;
        _icon.Value = data.Icon;
        _sprite = data.Sprite;
        _useType = data.UseType;
    }

    /// <summary>
    /// �Z���������̂��g�p���čU������
    /// </summary>
    public void Attack()
    {
        Bullet blt = Bullet.Init(_bulletPrefab, _sprite, _useType, _damage);

    }

    /// <summary>
    /// �f�[�^�������������Ă��邩���m���߂�e�X�g�p�֐�
    /// </summary>
    public void CheckData()
    {
        Debug.Log($"Name;{_name} UseType:{_useType} Damage:{_damage}");
    }
}
