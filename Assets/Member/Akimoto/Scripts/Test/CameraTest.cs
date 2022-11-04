using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CameraTest : MonoBehaviour
{
    [SerializeField] CameraController _cam;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _moveSpeed;

    private void Start()
    {
        SetLookAtObj();
    }

    private void Update()
    {
        Vector2 v = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rb.velocity = v * _moveSpeed;
    }

    public async void SetLookAtObj()
    {
        await UniTask.Delay(3000);
        _cam.SetPosition = new Vector2(5, 0);
        await UniTask.Delay(3000);
        _cam.SetPosition = Vector2.zero;
        await UniTask.Delay(3000);
        _cam.SetPosition = new Vector2(-5, 0);
        await UniTask.Delay(3000);
        _cam.SetPosition = Vector2.zero;
    }
}
