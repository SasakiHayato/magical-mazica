using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ObjectPool;

/// <summary>
/// �t�B�[���h��Ƀh���b�v����f�ރI�u�W�F�N�g
/// </summary>
public class FusionMaterialObject : DropObjectBase
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private RawMaterialDatabase _materialData;

    /// <summary>
    /// �f�ރI�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="original">������(Prefab)</param>
    /// <param name="createPosition">�������W</param>
    /// <param name="data">��������f�ރf�[�^</param>
    /// <param name="player">���݂̃v���C���[</param>
    /// <returns></returns>
    public static FusionMaterialObject Init(FusionMaterialObject original, Vector2 createPosition, RawMaterialDatabase data, Player player = null)
    {
        if (data == null) return null;

        FusionMaterialObject ret = Instantiate(original, createPosition, Quaternion.identity);
        ret.Setup(data, player);
        Debug.Log($"�h���b�v�����f��{data.Name}");
        return ret;
    }

    private void Setup(RawMaterialDatabase data, Player player)
    {
        _spriteRenderer.sprite = data.Sprite;
        _spriteRenderer.color = data.SpriteColor;
        _materialData = data;

        SubscribeApproachingEvent(player.gameObject);
        _approachingDropObject.SetAction = () =>
        {
            player.Storage.AddMaterial(_materialData.ID, 1);
            Destroy(gameObject);
        };
    }
}
