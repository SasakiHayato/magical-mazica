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
    /// <summary>IDから素材データを取得する</summary>
    public RawMaterialDatabase GetMaterialData(RawMaterialID rawMaterialID)
    {
        foreach (var data in _datas)
        {
            if (data.ID == rawMaterialID)
            {
                return data;
            }
        }

        return null;
    }

    public RawMaterialDatabase GetMaterialDataRandom()
    {
        int id = Random.Range(0, _datas.Count);

        return _datas.Find(d => d.ID == (RawMaterialID)System.Enum.ToObject(typeof(RawMaterialID), id));
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

    [SerializeField, Tooltip("表示するSpriteの色")]
    Color _spriteColor;

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
    /// <summary>画像の色</summary>
    public Color SpriteColor => _spriteColor;
    /// <summary>基礎ダメージ</summary>
    public int BaseDamage => _baseDamage;
}

/// <summary>
/// 素材ID
/// </summary>
public enum RawMaterialID
{
    /// <summary>null</summary>
    Empty = -1,   
    /// <summary>爆発</summary>
    BombBean,
    /// <summary>火力</summary>
    PowerPlant,
    /// <summary>貫通</summary>
    Penetration,
    /// <summary>毒</summary>
    Poison
}