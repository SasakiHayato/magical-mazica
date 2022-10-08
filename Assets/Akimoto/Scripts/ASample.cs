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
    [SerializeField] FusionItem _fusionItem;

    private void Start()
    {
        _fusionItem.Setup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("融合開始");
            _fusionItem.Fusion(RawMaterialID.PowerPlant, RawMaterialID.BombBean);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("攻撃");
            _fusionItem.Attack(Vector2.right * 5);
        }
    }
}
