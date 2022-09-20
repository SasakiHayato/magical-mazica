using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 秋元のサンプルクラス
/// </summary>
public class ASample : MonoBehaviour
{
    [SerializeField] RawMaterialID _materialID1;
    [SerializeField] RawMaterialID _materialID2;
    [SerializeField] FusionData _fusionData;
    private FusionItem _fusionItem;

    private void Start()
    {
        _fusionItem = _fusionData.GetFusionData(_materialID1, _materialID2);

        _fusionItem.CheckData();
    }
}
