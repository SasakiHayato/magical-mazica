using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Z����A�C�e���̃f�[�^�̏W�܂�
/// </summary>
[CreateAssetMenu(fileName = "Fusion Data")]
public class FusionData : ScriptableObject
{
    [SerializeField] RawMaterialData _rawMaterialData;
    [SerializeField] List<FusionDatabase> _datas;
    /// <summary>
    /// �g�p�����f�ނƈ�v�����A�C�e����n��
    /// </summary>
    /// <param name="rawMaterialID1"></param>
    /// <param name="rawMaterialID2"></param>
    /// <returns>�Z����A�C�e��</returns>
    public FusionDatabase GetFusionData(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2, ref int damage)
    {
        foreach (var data in _datas)
        {
            if (data.IsFusionSuccess(rawMaterialID1, rawMaterialID2))
            {
                //�Z�����Ɏg�p�����f�ނ���U���͂��Z�o
                damage = _rawMaterialData.GetMaterialData(rawMaterialID1).BaseDamage + _rawMaterialData.GetMaterialData(rawMaterialID2).BaseDamage;
                
                return data;
            }
        }
        throw new System.Exception($"�Z���f��[{rawMaterialID1}]��[{rawMaterialID2}]�Ɉ�v�����f�[�^�͌�����܂���ł���");
    }

}

/// <summary>�Z����̃A�C�e���f�[�^</summary>
[System.Serializable]
public class FusionDatabase
{
    [SerializeField] string _name;
    [SerializeField] Sprite _icon;
    [SerializeField] Sprite _sprite;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
    [SerializeField] float _bulletSpeed;
    /// <summary>���O</summary>
    public string Name => _name;
    /// <summary>���̃A�C�e���̃A�C�R���摜</summary>
    public Sprite Icon => _icon;
    /// <summary>���̃A�C�e�����g�p���̉摜</summary>
    public Sprite Sprite => _sprite;
    /// <summary>�g�p�����Ƃ��̐U�镑��</summary>
    public UseType UseType => _useType;
    /// <summary>�e�̔�ԑ��x</summary>
    public float BulletSpeed => _bulletSpeed;
    /// <summary>�Z�������ۂ̔��������</summary>
    /// <returns>�Z�������̉�</returns>
    public bool IsFusionSuccess(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2)
    {
        return _fusionUseMaterials.IsFusionSuccess(rawMaterialID1, rawMaterialID2);
    }
}

/// <summary>�Z���Ɏg�p����f�ނ�I������N���X</summary>
[System.Serializable]
public class FusionUseRawMaterial
{
    [SerializeField] RawMaterialID _materialData1;
    [SerializeField] RawMaterialID _materialData2;

    /// <summary>�Z�������ۂ̔��������</summary>
    /// <returns>�Z�������̉�</returns>
    public bool IsFusionSuccess(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2)
    {
        if (_materialData1 == rawMaterialID1)
        {
            if (_materialData2 == rawMaterialID2)
            {
                return true;
            }
        }
        if (_materialData1 == rawMaterialID2)
        {
            if (_materialData2 == rawMaterialID1)
            {
                return true;
            }
        }
        return false;
    }
}

/// <summary>
/// �A�C�e���g�p��̐U�镑��
/// </summary>
public enum UseType
{
    /// <summary>��������`���Ĕ��ł���<br/>�����ɓ�����Ə���</summary>
    Throw,
    /// <summary>�����ɔ��ōs��<br/>�����ɓ�����Ə���</summary>
    Strike,
}
