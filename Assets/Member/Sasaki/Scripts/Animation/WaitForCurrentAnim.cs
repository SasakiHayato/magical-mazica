using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForCurrentAnim : CustomYieldInstruction
{
    int _hash = 0;

    Animator _anim;

    public WaitForCurrentAnim(Animator anim)
    {
        _anim = anim;
        _hash = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
    }

    public override bool keepWaiting
    {
        get
        {
            var currentAnimState = _anim.GetCurrentAnimatorStateInfo(0);
            return currentAnimState.fullPathHash == _hash && currentAnimState.normalizedTime < 1;
        }
    }
}
