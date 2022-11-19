using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerDisposer : MonoBehaviour
{
    public System.Action Action { get; set; }

    void OnDestroy()
    {
        Action.Invoke();
    }
}
