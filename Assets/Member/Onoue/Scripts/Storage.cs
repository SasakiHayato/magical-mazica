using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Storage : MonoBehaviour
{
    /// <summary>素材のID、素材を持っている数 </summary>
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

    //public void SetMaterial(List<RawMaterialID> materialIDs)
    //{
    //    _materialCount.Add(RawMaterialID.BombBean, 200);
    //    _materialCount.Add(RawMaterialID.PowerPlant, 400);
    //    _materialCount.Add(RawMaterialID.Empty, 0);
    //    _materialCount.Add(RawMaterialID.Empty, 0);
    //}

    public int GetCount(RawMaterialID id)
    {
        return _materialCount[id];
    }

    /// <summary>
    /// 素材の追加<br/>
    /// 既に素材を所持していたら所持数を増やし、持ってなかったら新たに追加する
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public void AddMaterial(RawMaterialID id, int value)
    {
        if (_materialCount.TryGetValue(id, out int a))
        {
            _materialCount[id] += value;
        }
        else
        {
            //Addじゃイベントが発行されなかった
            _materialCount.Add(id, 0);
            _materialCount[id] = value;
        }
    }
}
