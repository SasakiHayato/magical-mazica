using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class EffectRequester
{
    class EffectPoolData
    {
        public EffectPoolData(string path)
        {
            Path = path;
            EffctPool = new Pool<Effecter>();
        }

        public string Path { get; private set; }
        public Pool<Effecter> EffctPool { get; private set; }
    }

    EffectDataAsset _dataAsset;
    List<EffectPoolData> _effectData = new List<EffectPoolData>();

    public EffectRequester LoadAsset()
    {
        _dataAsset = Resources.Load<EffectDataAsset>("EffectDataAsset");
        return this;
    }

    public EffectRequester SetupEffect(string path, Transform user)
    {
        Effecter effecter = _dataAsset.GetEffect(path);

        EffectPoolData data = new EffectPoolData(path);
        data.EffctPool
                .SetMono(effecter)
                .IsSetParent(user)
                .CreateRequest();

        _effectData.Add(data);

        return this;
    }

    public void Use(string path, Vector2 position)
    {
        EffectPoolData data = _effectData.Find(e => e.Path == path);
        Effecter effecter = data.EffctPool.UseRequest();

        effecter.transform.position = position;

    }
}
