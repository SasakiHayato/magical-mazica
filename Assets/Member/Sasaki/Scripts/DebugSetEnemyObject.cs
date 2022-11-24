using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetEnemyObject : MonoBehaviour
{
    [SerializeField] Transform _parent;

    static DebugSetEnemyObject Instance;

    void Awake()
    {
        Instance = this;
    }

    public static void SetEnemy(Transform target)
    {
        if (Instance == null) return; 
        
        target.SetParent(Instance._parent);
    }
}
