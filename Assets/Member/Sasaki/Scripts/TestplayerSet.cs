using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestplayerSet : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void Awake()
    {
        GameController.Instance.Player = _target;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
