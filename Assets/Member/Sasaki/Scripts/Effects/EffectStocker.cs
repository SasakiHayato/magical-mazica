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

    public void LoadEffect(string path, Vector2 position)
    {
        StockData data = _stockDataList.FirstOrDefault(e => e.Path == path);

        if (data != null)
        {
            Effect effect = data.Pool.UseRequest();
            effect.transform.position = position;
        }
    }

    public void AddFieldEffect(FieldEffect.EffectType effectType, IFieldEffectable effectable)
    {
        _fieldEffect.AddEffect(effectType, effectable);
    }

    public void LoadFieldEffect(FieldEffect.EffectType effectType)
    {
        _fieldEffect.LoadEffect(effectType);
    }
}
