using System.Collections.Generic;
using UnityEngine;
using ObjectPool;
using System.Linq;

public class EffectStocker : MonoBehaviour
{
    class StockData
    {
        public StockData(string path, Effect effect, Transform parent)
        {
            Path = path;
            Pool = new Pool<Effect>();
            Pool
                .SetMono(effect)
                .IsSetParent(parent)
                .CreateRequest();
        }
        
        public string Path { get; private set; }
        public Pool<Effect> Pool { get; private set; }
    }

    [SerializeField] EffectDataAsset _effectDataAsset;

    List<StockData> _stockDataList = new List<StockData>();
    FieldEffect _fieldEffect = new FieldEffect();

    public static EffectStocker Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateStockData();
    }

    void CreateStockData()
    {
        foreach (EffectDataAsset.EffectData effect in _effectDataAsset.EffectDataList)
        {
            StockData data = new StockData(effect.Path, effect.Effect, transform);
            _stockDataList.Add(data);
        }
    }

    public Effect LoadEffect(string path, Vector2 position)
    {
        StockData data = _stockDataList.FirstOrDefault(e => e.Path == path);

        if (data != null)
        {
            Effect effect = data.Pool.UseRequest();
            effect.transform.position = position;

            return effect;
        }

        return null;
    }

    public Effect LoadEffect(string path, Transform parent)
    {
        StockData data = _stockDataList.FirstOrDefault(e => e.Path == path);

        if (data != null)
        {
            Effect effect = data.Pool.UseRequest();
            effect.transform.position = parent.position;

            effect.transform.SetParent(parent);

            return effect;
        }

        return null;
    }

    public void AddFieldEffect(IFieldEffectDatable effectable)
    {
        _fieldEffect.AddEffect(effectable);
    }

    public void LoadFieldEffect<Value>(FieldEffect.EffectType effectType, Value value)
    {
        _fieldEffect.LoadEffect(effectType, value);
    }
}
