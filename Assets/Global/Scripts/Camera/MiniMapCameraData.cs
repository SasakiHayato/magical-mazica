using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MiniMapCameraData
{
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] LayerMask _viewLayer;
    [SerializeField] RenderTexture _renderTexture;

    public Vector3 OffsetPosition => _offsetPosition;
    public LayerMask ViewLayer => _viewLayer;
    public RenderTexture RenderTexture => _renderTexture;
}
