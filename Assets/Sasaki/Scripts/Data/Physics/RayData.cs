using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// �n�ʂƂ̐ڒn����Ɏg��Ray�̃f�[�^
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