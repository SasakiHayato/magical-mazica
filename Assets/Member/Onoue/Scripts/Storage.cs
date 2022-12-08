using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Storage : MonoBehaviour
{
    /// <summary>ëfçﬁÇÃIDÅAëfçﬁÇéùÇ¡ÇƒÇ¢ÇÈêî/// </summary>
    ReactiveDictionary<RawMaterialID, int> _materialCount = new ReactiveDictionary<RawMaterialID,int>();
    public System.IObservable<DictionaryReplaceEvent<RawMaterialID, int>> MaterialDictionary => _materialCount.ObserveReplace();
    
    private void Start()
    {
    }

    void Test()
    {
        _materialCount[RawMaterialID.BombBean] = 2;
        _materialCount[RawMaterialID.PowerPlant] = 0;
    }

    public void SetMaterial(List<RawMaterialID> materialIDs)
    {
        _materialCount.Add(RawMaterialID.BombBean, 200);
        _materialCount.Add(RawMaterialID.PowerPlant, 400);
        _materialCount.Add(RawMaterialID.Empty, 0);
        _materialCount.Add(RawMaterialID.Empty, 0);
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
