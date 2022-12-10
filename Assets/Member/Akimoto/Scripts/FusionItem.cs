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
    [SerializeField] float _throwBulletHeightOffset;
    /// <summary>�A�C�e����</summary>
    private ReactiveProperty<string> _name = new ReactiveProperty<string>();
    /// <summary>�A�C�R��</summary>
    private ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();
    /// <summary>�_���[�W</summary>
    private int _damage;
    /// <summary>���ݕێ����Ă���Z���f�[�^�x�[�X</summary>
    private FusionDatabase _database;
    public System.IObservable<string> ChangeNameObservable => _name;
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

        _name.Value = data.Name;
        _icon.Value = data.Icon;
        _database = data;
    }

    /// <summary>
    /// �Z���������̂��g�p���čU������<br/>
    /// �U�����Ƀv���C���[�N���X���炱�̊֐����ĂԂ���
    /// </summary>
    public void Attack(Vector2 directions)
    {
        //�Z���O�͍U���s��
        if (_database == null)
        {
            Debug.Log("��ɗZ�����Ă�������");
            return;
        }
        //����
        Bullet blt = Bullet.Init(_bulletPrefab, _database, _damage);
        blt.transform.position = transform.position;
        blt.Velocity = BulletDirectionOffset(directions, blt) * _database.BulletSpeed;
        Dispose();
    }

    private Vector2 BulletDirectionOffset(Vector2 direction, Bullet bullet)
    {
        Vector2 ret = new Vector2(direction.x, direction.y);
        switch (bullet.UseType)
        {
            case BulletType.Throw:
                ret = new Vector2(direction.x, direction.y + _throwBulletHeightOffset);
                break;
            case BulletType.Strike:
                break;
        }
        return ret;
    }

    /// <summary>
    /// �f�[�^�̔j��
    /// </summary>
    private void Dispose()
    {
        _database = null;
        _name.Value = "";
        _icon.Value = null;
    }
}
