using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StatusEffects { }

public class Poison : StatusEffectBase
{
    public override int Effect(System.Func<int> method, EffectEvaluationStatType evaluationStatType)
    {
        throw new System.NotImplementedException();
    }
}
