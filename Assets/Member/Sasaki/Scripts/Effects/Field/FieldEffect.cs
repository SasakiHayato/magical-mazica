using System.Collections.Generic;
using System.Linq;

public interface IFieldEffectDatable
{
    FieldEffect.EffectType EffectType { get; }
}

public interface IFieldEffectable<Value> : IFieldEffectDatable
{
    void Execute(Value value);
}

public class FieldEffect
{
    public enum EffectType
    {
        HitStop,
        CmShake,
    }

    List<IFieldEffectDatable> _fieldEffectDataList = new List<IFieldEffectDatable>();
    Dictionary<EffectType, IFieldEffectDatable> _effectDic = new Dictionary<EffectType, IFieldEffectDatable>();
    
    public void AddEffect(IFieldEffectDatable executable)
    {
        if (_effectDic.Count <= 0)
        {
            _fieldEffectDataList.Add(executable);
            //_effectDic.Add(effectType, executable);
        }

        if (!_fieldEffectDataList.Any(e => e.EffectType == executable.EffectType))
        {
            _fieldEffectDataList.Add(executable);
            //_effectDic.Add(effectType, executable);
        }
    }

    public void LoadEffect<Value>(EffectType effectType, Value value)
    {
        IFieldEffectDatable datable = _fieldEffectDataList.FirstOrDefault(f => f.EffectType == effectType);
        IFieldEffectable<Value> effectable = datable as IFieldEffectable<Value>;

        if (effectable != null)
        {
            effectable.Execute(value);
        }

        //_effectDic.FirstOrDefault(e => e.Key == effectType).Value?.Execute(value);
    }
}
