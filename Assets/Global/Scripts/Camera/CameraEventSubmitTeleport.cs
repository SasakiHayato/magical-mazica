using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventSubmitTeleport : ICamearaEventable
{
    void ICamearaEventable.DisposeEvent()
    {
        
    }

    void ICamearaEventable.OnEvent()
    {
        
    }

    bool ICamearaEventable.OnExecute()
    {
        return Teleporter.OnSelect;
    }

    void ICamearaEventable.Setup(Camera camera)
    {
        
    }
}
