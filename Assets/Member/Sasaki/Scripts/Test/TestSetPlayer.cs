using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetPlayer : MonoBehaviour
{
    [SerializeField] GameObject _player;

    public GameObject Player => _player;
    public static TestSetPlayer Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
