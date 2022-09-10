using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// 地面を感知するためのデータ
    /// </summary>
    [System.Serializable]
    public class GroundData
    {
        [SerializeField] bool _isDebug;
        [SerializeField] Transform _core;

        [SerializeField] LayerMask _groundLayer;
        [SerializeField] List<RayData> _rayDataList;

        /// <summary>
        /// 地面との接地判定
        /// </summary>
        public bool IsGround => _rayDataList.Any(d => Process(d));

        bool Process(RayData rayData)
        {
            Vector2 corePos = _core.position;
            corePos += rayData.OffsetPos;

            if (_isDebug)
            {
                Debug.DrawLine(corePos, rayData.Direction * rayData.Distance + corePos, Color.red);
            }

            return Physics2D.Raycast(corePos, rayData.Direction, rayData.Distance, _groundLayer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void SetUp(Transform user)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"User:{user.name} InstanceID:{user.gameObject.GetInstanceID()}");

            if (_core == null)
            {
                _core = user;
                builder.Append("\n");
                builder.Append($"CoreがNullです。{user.name}のTranceformを設定しました。");
            }

            if (_groundLayer.value == 0)
            {
                _groundLayer.value = 1;
                builder.Append("\n");
                builder.Append($"GroundlayerがNothingです。LayerをDefaultに設定しました。");
            }

            if (_isDebug)
            {
                Debug.Log(builder.ToString());
            }
        }
    }
}