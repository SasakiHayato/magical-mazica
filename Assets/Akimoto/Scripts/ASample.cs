using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �H���̃T���v���N���X
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
            Debug.Log("�Z���J�n");
            _fusionItem.Fusion(RawMaterialID.PowerPlant, RawMaterialID.BombBean);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("�U��");
            _fusionItem.Attack(Vector2.right * 5);
        }
    }
}
