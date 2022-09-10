using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// 重力データ
    /// </summary>

    [System.Serializable]
    public class GravityData
    {
        [SerializeField] Vector2 _dir;
        [SerializeField] float _scale;

        public Vector2 Direction => _dir.normalized;
        public float Scale => _scale;

        public float Gravity = 0;
    }
}