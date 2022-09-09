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
    [SerializeField] List<FisionDatabase> _datas;
}

/// <summary>�Z����̃A�C�e���f�[�^</summary>
[System.Serializable]
public class FisionDatabase
{
    [SerializeField] string _name;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
}

/// <summary>�Z���Ɏg�p����f�ނ�I������N���X</summary>
[System.Serializable]
public class FusionUseRawMaterial
{
    [SerializeField] RawMaterialID _materialData1;
    [SerializeField] RawMaterialID _materialData2;
    /// <summary>�g�p����f�ނP</summary>
    public RawMaterialID MaterialData1 => _materialData1;
    /// <summary>�g�p����f�ނQ</summary>
    public RawMaterialID MaterialData2 => _materialData2;
}

/// <summary>
/// �A�C�e���g�p��̐U�镑��
/// </summary>
public enum UseType
{

}
