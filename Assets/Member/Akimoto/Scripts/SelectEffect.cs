using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SelectEffect
{

    public static StatusEffectBase AssignmentEffect(StatusEffectID statusEffectID, float time)
    {
        StatusEffectBase ret;
        switch (statusEffectID)
        {
            case StatusEffectID.Posion:
                ret = new Poison();
                ret.Time = time;
                break;
            default:
                Debug.LogError("–¢Š„“–‚ÌID‚ª“n‚³‚ê‚Ü‚µ‚½");
                ret = null;
                break;
        }
        return ret;
    }
}
