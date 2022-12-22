using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetplayer : MonoBehaviour
{
    private void Awake()
    {
        GameController.Instance.SetPlayer = transform;
    }
}
