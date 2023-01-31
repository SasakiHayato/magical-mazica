using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Z���f�ނ̃f�[�^�̏W�܂�
/// </summary>
[CreateAssetMenu(fileName = "RawMaterial Data")]
public class RawMaterialData : ScriptableObject
{
    [SerializeField] List<RawMaterialDatabase> _datas;
    /// <summary>ID����f�ރf�[�^���擾����</summary>
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

/// <summary>�Z���Ɏg�p����f�ނ̃f�[�^</summary>
[System.Serializable]
public class RawMaterialDatabase
{
    
    [SerializeField]
    string _name;

    [SerializeField]
    RawMaterialID�@_id;

    [SerializeField, TextArea]
    string _description;

    [SerializeField]
    Sprite _sprite;

    [SerializeField, Tooltip("�\������Sprite�̐F")]
    Color _spriteColor;

    /// <summary>�Z����̃A�C�e���g�p���̃G�t�F�N�g</summary>
    //[SerializeField]
    // ParticleID 

    [SerializeField, Tooltip("��b�_���[�W\n�Z�����ɑ������킳�ꂽ���̂��Z����̍U���͂ƂȂ�")]
    int _baseDamage;

    //�����Ƀo�t�f�o�t

    /// <summary>���O</summary>
    public string Name => _name;
    /// <summary>�f��ID</summary>
    public RawMaterialID ID => _id;
    /// <summary>������</summary>
    public string Description => _description;
    /// <summary>�摜</summary>
    public Sprite Sprite => _sprite;
    /// <summary>�摜�̐F</summary>
    public Color SpriteColor => _spriteColor;
    /// <summary>��b�_���[�W</summary>
    public int BaseDamage => _baseDamage;
}

/// <summary>
/// �f��ID
/// </summary>
public enum RawMaterialID
{
    /// <summary>null</summary>
    Empty = -1,   
    /// <summary>����</summary>
    BombBean,
    /// <summary>�Η�</summary>
    PowerPlant,
    /// <summary>�ђ�</summary>
    Penetration,
    /// <summary>��</summary>
    Poison
}