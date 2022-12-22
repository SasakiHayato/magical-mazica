using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// 秋元のサンプルクラス
/// </summary>
public class ASample : MonoBehaviour
{
    [SerializeField] GameObject _particle;
    [SerializeField] Transform _transform;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(_particle, _transform);
        }
    }
}
