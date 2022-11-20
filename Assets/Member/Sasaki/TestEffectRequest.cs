using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffectRequest : MonoBehaviour
{
    [SerializeField] string _path;

    public void Request()
    {
        EffectStocker.LoadEffect(_path, Vector2.zero);
    }
}
