using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 融合後アイテムのデータの集まり
/// </summary>
[CreateAssetMenu(fileName = "Fusion Data")]
public class FusionData : ScriptableObject
{
    [SerializeField] RawMaterialData _rawMaterialData;
    [SerializeField] List<FisionDatabase> _datas;
}

/// <summary>融合後のアイテムデータ</summary>
[System.Serializable]
public class FisionDatabase
{
    [SerializeField] string _name;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
}

/// <summary>融合に使用する素材を選択するクラス</summary>
[System.Serializable]
public class FusionUseRawMaterial
{
    [SerializeField] RawMaterialID _materialData1;
    [SerializeField] RawMaterialID _materialData2;
    /// <summary>使用する素材１</summary>
    public RawMaterialID MaterialData1 => _materialData1;
    /// <summary>使用する素材２</summary>
    public RawMaterialID MaterialData2 => _materialData2;
}

/// <summary>
/// アイテム使用後の振る舞い
/// </summary>
public enum UseType
{

}
