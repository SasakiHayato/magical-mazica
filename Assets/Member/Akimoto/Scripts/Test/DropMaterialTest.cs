using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class DropMaterialTest : MonoBehaviour
{
    [SerializeField] FusionMaterialObject _prefab;
    [SerializeField] Player _player;
    [SerializeField] RawMaterialData _rawMaterialData;

    private void Start()
    {
        Player p = Instantiate(_player, new Vector2(4, 5), Quaternion.identity);
        FusionMaterialObject.Init(_prefab, Vector2.zero, _rawMaterialData.GetMaterialData(RawMaterialID.BombBean), p);
    }
}
