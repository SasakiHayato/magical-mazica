using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraData
{
    [System.Serializable]
    public class ShakeData
    {
        [SerializeField] float _time;
        [SerializeField] float _power;

        public float Time => _time;
        public float Power => _power;

        public float GetMagnitude => Random.Range(-1, 1);
    }

    [SerializeField] ShakeData _shakeData;

    public ShakeData Shake => _shakeData;
}
