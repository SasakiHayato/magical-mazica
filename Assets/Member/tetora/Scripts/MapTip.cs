using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTip : MonoBehaviour
{
    [SerializeField]
    bool _isFirstMake;//true:作れる false:作れない
    public bool IsFirstMake { get => _isFirstMake; }
}
