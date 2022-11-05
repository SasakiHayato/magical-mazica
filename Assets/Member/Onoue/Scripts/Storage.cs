using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    /// <summary>�f�ނ�ID�A�f�ނ������Ă��鐔/// </summary>
    Dictionary<RawMaterialID, int> _materialCount = new Dictionary<RawMaterialID, int>();
    public Dictionary<RawMaterialID, int> MaterialCount { get => _materialCount; private set { } }
    private void Start()
    {
        SetUp();
    }
    void SetUp()
    {
        _materialCount[RawMaterialID.BombBean] = 2;
        _materialCount[RawMaterialID.PowerPlant] = 0;
    }

}
