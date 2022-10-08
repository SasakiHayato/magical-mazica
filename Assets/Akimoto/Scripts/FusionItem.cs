using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �Z���A�C�e���̊Ǘ��N���X<br/>�v���C���[�X�N���v�g�ƈꏏ�ɕt���Ďg��
/// </summary>
public class FusionItem : MonoBehaviour
{
    [SerializeField] FusionData _fusionData;
    [SerializeField] Bullet _bulletPrefab;
    /// <summary>�A�C�e����</summary>
    private string _name;
    /// <summary>�A�C�R��</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>�_���[�W</summary>
    private int _damage;
    /// <summary>���ݕێ����Ă���Z���f�[�^�x�[�X</summary>
    private FusionDatabase _database;
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
        _database = data;
    }

    /// <summary>
    /// �Z���������̂��g�p���čU������<br/>
    /// �U�����Ƀv���C���[�N���X���炱�̊֐����ĂԂ���
    /// </summary>
    public void Attack(Vector2 directions)
    {
        Bullet blt = Bullet.Init(_bulletPrefab, _database, _damage);
        blt.transform.position = transform.position;
        blt.Velocity = directions;
    }
}
