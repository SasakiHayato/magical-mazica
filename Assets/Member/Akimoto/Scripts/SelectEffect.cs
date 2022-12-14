using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SelectEffect
{

    public static StatusEffectBase AssignmentEffect(StatusEffectID statusEffectID, int time)
    {
        StatusEffectBase ret;
        switch (statusEffectID)
        {
            case StatusEffectID.Posion:
                ret = new Poison();
                ret.Time = time;
                break;
            default:
                Debug.LogError("未割当のIDが渡されました");
                ret = null;
                break;
        }
        return ret;
    }
}
