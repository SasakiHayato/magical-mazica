using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// Cinemachineのカメラ制御
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _lookAt;

    /// <summary>
    /// カメラの追従先のオブジェクトのLocalPosition
    /// </summary>
    public Vector2 CameraLocalPosition { get => _lookAt.localPosition; set => _lookAt.localPosition = value; }
}
