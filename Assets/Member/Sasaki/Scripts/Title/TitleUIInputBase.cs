using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TitleUIInputBase : IUIOperateEventable, IGameSetupable
{
    int IGameSetupable.Priority => 1;

    void IGameSetupable.GameSetup()
    {
        
    }

    void IUIOperateEventable.CancelEvent()
    {
        throw new System.NotImplementedException();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        throw new System.NotImplementedException();
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        throw new System.NotImplementedException();
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        throw new System.NotImplementedException();
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        return true;
    }
}
