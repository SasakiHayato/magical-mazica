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
}

/// <summary>融合に使用する素材のデータ</summary>
[System.Serializable]
public class RawMaterialDatabase
{
    /// <summary>名前</summary>
    [SerializeField]
        string _name;
    /// <summary>説明文</summary>
    [SerializeField, TextArea]
        string _description;
    /// <summary>画像</summary>
    [SerializeField]
        Sprite _sprite;
    /// <summary>融合後のアイテム使用時のエフェクト</summary>
    [SerializeField]
        GameObject _particlePrefab;
    /// <summary>基礎ダメージ</summary>
    [SerializeField, Tooltip("基礎ダメージ\n融合時に足し合わされたものが融合後の攻撃力となる")]
        int _baseDamage;
    //バフデバフ
}

/// <summary>
/// 素材ID
/// </summary>
public enum RawMaterialID
{

}