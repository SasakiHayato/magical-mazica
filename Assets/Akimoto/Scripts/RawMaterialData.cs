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
}

/// <summary>�Z���Ɏg�p����f�ނ̃f�[�^</summary>
[System.Serializable]
public class RawMaterialDatabase
{
    /// <summary>���O</summary>
    [SerializeField]
        string _name;
    /// <summary>������</summary>
    [SerializeField, TextArea]
        string _description;
    /// <summary>�摜</summary>
    [SerializeField]
        Sprite _sprite;
    /// <summary>�Z����̃A�C�e���g�p���̃G�t�F�N�g</summary>
    [SerializeField]
        GameObject _particlePrefab;
    /// <summary>��b�_���[�W</summary>
    [SerializeField, Tooltip("��b�_���[�W\n�Z�����ɑ������킳�ꂽ���̂��Z����̍U���͂ƂȂ�")]
        int _baseDamage;
    //�o�t�f�o�t
}

/// <summary>
/// �f��ID
/// </summary>
public enum RawMaterialID
{

}