using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BulletTest : MonoBehaviour
{
    //[SerializeField] FusionData _futionData;
    [SerializeField] RawMaterialData _materialData;
    //[SerializeField] Bullet _bullet;
    [SerializeField] FusionItem _fusionItem;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _fusionItem.Fusion(RawMaterialID.BombBean, RawMaterialID.PowerPlant);
            _fusionItem.Attack(Vector2.right);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            _fusionItem.Fusion(RawMaterialID.BombBean, RawMaterialID.BombBean);
            _fusionItem.Attack(Vector2.right);
        }
    }
}
