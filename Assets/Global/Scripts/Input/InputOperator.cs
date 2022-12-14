using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOperator : MonoBehaviour
{
    System.Action _action;

    public void Setup(System.Action action)
    {
        _action = action;
    }

    void Update()
    {
        _action?.Invoke();
    }
}
