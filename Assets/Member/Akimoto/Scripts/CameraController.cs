using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// Cinemachine�̃J��������
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _lookAt;

    /// <summary>
    /// �J�����̒Ǐ]��̃I�u�W�F�N�g��LocalPosition
    /// </summary>
    public Vector2 CameraLocalPosition { get => _lookAt.localPosition; set => _lookAt.localPosition = value; }
}
