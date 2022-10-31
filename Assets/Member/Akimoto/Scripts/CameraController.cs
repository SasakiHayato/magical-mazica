using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _lookAt;

    //public void Setup()
    //{
    //}

    public Vector2 SetPosition
    {
        set
        {
            _lookAt.localPosition = value;
        }
    }
}
