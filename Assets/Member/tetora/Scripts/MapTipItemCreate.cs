using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTipItemCreate : MonoBehaviour
{
    [SerializeField] 
    FusionMaterialObject _materialObject;
    [SerializeField] 
    RawMaterialData _materialData;
    void Start()
    {
        SetUp();
    }
    void SetUp()
    {
        RawMaterialDatabase rawMaterial = _materialData.GetMaterialDataRandom();

        Player player = GameController.Instance.Player.GetComponent<Player>();
        FusionMaterialObject.Init(_materialObject, transform.position, rawMaterial, player);
    }
}
