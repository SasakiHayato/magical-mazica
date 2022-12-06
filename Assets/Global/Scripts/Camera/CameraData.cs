using UnityEngine;

[System.Serializable]
public class CameraData
{
    [System.Serializable]
    public class ViewData
    {
        [SerializeField] Vector3 _offset;
        
        public Vector3 Offset => _offset;
    }

    [System.Serializable]
    public class ShakeData
    {
        [SerializeField] float _time;
        [SerializeField] float _power;

        public float Time => _time;
        public float Power => _power;

        public float GetMagnitude => Random.Range(-1, 1);
    }

    [SerializeField] ViewData _viewData;
    [SerializeField] ShakeData _shakeData;

    public ViewData View => _viewData;
    public ShakeData Shake => _shakeData;
}
