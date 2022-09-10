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
    [SerializeField] List<FusionDatabase> _datas;
    public FusionDatabase GetFusionData(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2)
    {
        return null;
    }

}

/// <summary>融合後のアイテムデータ</summary>
[System.Serializable]
public class FusionDatabase
{
    [SerializeField] string _name;
    [SerializeField] Sprite _sprite;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
    /// <summary>名前</summary>
    public string Name => _name;
    /// <summary>使用したときの振る舞い</summary>
    public UseType UseType => _useType;
    /// <summary>融合成功可否の判定をする</summary>
    /// <returns>融合成功の可否</returns>
    public bool IsFusionSuccess(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2) =>
        _fusionUseMaterials.IsFusionSuccess(rawMaterialID1, rawMaterialID2);
}

/// <summary>融合に使用する素材を選択するクラス</summary>
[System.Serializable]
public class FusionUseRawMaterial
{
    [SerializeField] RawMaterialID _materialData1;
    [SerializeField] RawMaterialID _materialData2;
    /// <summary>融合成功可否の判定をする</summary>
    /// <returns>融合成功の可否</returns>
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
/// アイテム使用後の振る舞い
/// </summary>
public enum UseType
{

}
