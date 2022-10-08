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
    /// <summary>
    /// 使用した素材と一致したアイテムを渡す
    /// </summary>
    /// <param name="rawMaterialID1"></param>
    /// <param name="rawMaterialID2"></param>
    /// <returns>融合後アイテム</returns>
    public FusionDatabase GetFusionData(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2, ref int damage)
    {
        foreach (var data in _datas)
        {
            if (data.IsFusionSuccess(rawMaterialID1, rawMaterialID2))
            {
                //融合元に使用した素材から攻撃力を算出
                damage = _rawMaterialData.GetMaterialData(rawMaterialID1).BaseDamage + _rawMaterialData.GetMaterialData(rawMaterialID2).BaseDamage;
                
                return data;
            }
        }
        throw new System.Exception($"融合素材[{rawMaterialID1}]と[{rawMaterialID2}]に一致したデータは見つかりませんでした");
    }

}

/// <summary>融合後のアイテムデータ</summary>
[System.Serializable]
public class FusionDatabase
{
    [SerializeField] string _name;
    [SerializeField] Sprite _icon;
    [SerializeField] Sprite _sprite;
    [SerializeField] FusionUseRawMaterial _fusionUseMaterials;
    [SerializeField] UseType _useType;
    [SerializeField] float _bulletSpeed;
    /// <summary>名前</summary>
    public string Name => _name;
    /// <summary>このアイテムのアイコン画像</summary>
    public Sprite Icon => _icon;
    /// <summary>このアイテムを使用時の画像</summary>
    public Sprite Sprite => _sprite;
    /// <summary>使用したときの振る舞い</summary>
    public UseType UseType => _useType;
    /// <summary>弾の飛ぶ速度</summary>
    public float BulletSpeed => _bulletSpeed;
    /// <summary>融合成功可否の判定をする</summary>
    /// <returns>融合成功の可否</returns>
    public bool IsFusionSuccess(RawMaterialID rawMaterialID1, RawMaterialID rawMaterialID2)
    {
        return _fusionUseMaterials.IsFusionSuccess(rawMaterialID1, rawMaterialID2);
    }
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
/// アイテム使用後の振る舞い
/// </summary>
public enum UseType
{
    /// <summary>放物線を描いて飛んでいく<br/>何かに当たると消滅</summary>
    Throw,
    /// <summary>直線に飛んで行く<br/>何かに当たると消滅</summary>
    Strike,
}
