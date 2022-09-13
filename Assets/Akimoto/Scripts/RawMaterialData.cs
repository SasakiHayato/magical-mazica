using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 融合素材のデータの集まり
/// </summary>
[CreateAssetMenu(fileName = "RawMaterial Data")]
public class RawMaterialData : ScriptableObject
{
    [SerializeField] List<RawMaterialDatabase> _datas;
    public RawMaterialDatabase GetMaterialData(RawMaterialID rawMaterialID)
    {
        foreach (var data in _datas)
        {
            if (data.ID == rawMaterialID)
            {
                return data;
            }
        }
        throw new System.Exception("指定された素材データが見つかりませんでした");
    }
}

/// <summary>融合に使用する素材のデータ</summary>
[System.Serializable]
public class RawMaterialDatabase
{
    
    [SerializeField]
    string _name;

    [SerializeField]
    RawMaterialID　_id;

    [SerializeField, TextArea]
    string _description;

    [SerializeField]
    Sprite _sprite;

    /// <summary>融合後のアイテム使用時のエフェクト</summary>
    //[SerializeField]
    // ParticleID 

    [SerializeField, Tooltip("基礎ダメージ\n融合時に足し合わされたものが融合後の攻撃力となる")]
    int _baseDamage;

    //ここにバフデバフ

    /// <summary>名前</summary>
    public string Name => _name;
    /// <summary>素材ID</summary>
    public RawMaterialID ID => _id;
    /// <summary>説明文</summary>
    public string Description => _description;
    /// <summary>画像</summary>
    public Sprite Sprite => _sprite;
    /// <summary>基礎ダメージ</summary>
    public int BaseDamage => _baseDamage;
}

/// <summary>
/// 素材ID
/// </summary>
public enum RawMaterialID
{
    /// <summary>爆裂豆</summary>
    BombBean,
    /// <summary>パワープラント</summary>
    PowerPlant,
}