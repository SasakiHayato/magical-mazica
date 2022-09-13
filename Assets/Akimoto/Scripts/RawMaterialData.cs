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
    public RawMaterialDatabase GetMaterialData(RawMaterialID rawMaterialID)
    {
        foreach (var data in _datas)
        {
            if (data.ID == rawMaterialID)
            {
                return data;
            }
        }
        throw new System.Exception("�w�肳�ꂽ�f�ރf�[�^��������܂���ł���");
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
    /// <summary>��b�_���[�W</summary>
    public int BaseDamage => _baseDamage;
}

/// <summary>
/// �f��ID
/// </summary>
public enum RawMaterialID
{
    /// <summary>������</summary>
    BombBean,
    /// <summary>�p���[�v�����g</summary>
    PowerPlant,
}