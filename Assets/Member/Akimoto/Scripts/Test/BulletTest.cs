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
            int i = 0;
            //Bullet bullet = Bullet.Init(_bullet, _futionData.GetFusionData(RawMaterialID.BombBean, RawMaterialID.PowerPlant, ref i), i);
            //bullet.
        }
    }
}
