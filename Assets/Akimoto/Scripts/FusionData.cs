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
    public FusionDatabase GetFusionData(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2)
    {
        return null;
    }

}

/// <summary>�Z����̃A�C�e���f�[�^</summary>
[System.Serializable]
public class FusionDatabase
{
    [SerializeField] string _name;
    [SerializeField] Sprite _sprite;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
    /// <summary>���O</summary>
    public string Name => _name;
    /// <summary>�g�p�����Ƃ��̐U�镑��</summary>
    public UseType UseType => _useType;
    /// <summary>�Z�������ۂ̔��������</summary>
    /// <returns>�Z�������̉�</returns>
    public bool IsFusionSuccess(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2) =>
        _fusionUseMaterials.IsFusionSuccess(rawMaterialID1, rawMaterialID2);
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
        if (_materialData1 == rawMaterialID1 || _materialData1 == rawMaterialID2)
        {
            if (_materialData2 == rawMaterialID1 || _materialData2 == rawMaterialID2)
            {

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

}
