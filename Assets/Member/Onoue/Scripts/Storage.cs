using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Storage : MonoBehaviour
{
    /// <summary>�f�ނ�ID�A�f�ނ������Ă��鐔/// </summary>
    ReactiveDictionary<RawMaterialID, int> _materialCount = new ReactiveDictionary<RawMaterialID,int>();
    public System.IObservable<DictionaryReplaceEvent<RawMaterialID, int>> Observable => _materialCount.ObserveReplace();
    private void Start()
    {
        _materialCount[RawMaterialID.Empty] = 1;
    }
    void Test()
    {
        _materialCount[RawMaterialID.BombBean] = 2;
        _materialCount[RawMaterialID.PowerPlant] = 0;
    }
    public int GetCount(RawMaterialID id)
    {
        return _materialCount[id];
    }
    public void AddMaterial(RawMaterialID id, int value)
    {
        _materialCount[id] += value;
    }
}
