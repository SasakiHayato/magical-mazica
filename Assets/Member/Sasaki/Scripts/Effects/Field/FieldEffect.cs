using System.Collections.Generic;
using System.Linq;

public interface IFieldEffectable
{
    void Execute();
}

public class FieldEffect
{
    public enum EffectType
    {
        HitStop,
        CmShake,
    }

    Dictionary<EffectType, IFieldEffectable> _effectDic = new Dictionary<EffectType, IFieldEffectable>();
    
    public void AddEffect(EffectType effectType, IFieldEffectable executable)
    {
        if (_effectDic.Count <= 0)
        {
            _effectDic.Add(effectType, executable);
        }

        if (!_effectDic.Any(e => e.Key == effectType))
        {
            _effectDic.Add(effectType, executable);
        }
    }

    public void LoadEffect(EffectType effectType)
    {
        _effectDic.FirstOrDefault(e => e.Key == effectType).Value?.Execute();
    }
}
