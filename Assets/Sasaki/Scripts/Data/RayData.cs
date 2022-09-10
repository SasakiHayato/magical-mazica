using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// 地面との接地判定に使うRayのデータ
    /// </summary>

    [System.Serializable]
    class RayData
    {
        [SerializeField] Vector2 _offsetPos;
        [SerializeField] Vector2 _rayDir;
        [SerializeField] float _distance;

        public Vector2 OffsetPos => _offsetPos;

        public Vector2 Direction => _rayDir.normalized;

        public float Distance => _distance;
    }
}