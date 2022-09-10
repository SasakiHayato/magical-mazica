using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomPhysics.Data
{
    /// <summary>
    /// �n�ʂ����m���邽�߂̃f�[�^
    /// </summary>
    [System.Serializable]
    public class GroundData
    {
        [SerializeField] bool _isDebug;
        [SerializeField] Transform _core;

        [SerializeField] LayerMask _groundLayer;
        [SerializeField] List<RayData> _rayDataList;

        /// <summary>
        /// �n�ʂƂ̐ڒn����
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
        /// ������
        /// </summary>
        public void SetUp(Transform user)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"User:{user.name} InstanceID:{user.gameObject.GetInstanceID()}");

            if (_core == null)
            {
                _core = user;
                builder.Append("\n");
                builder.Append($"Core��Null�ł��B{user.name}��Tranceform��ݒ肵�܂����B");
            }

            if (_groundLayer.value == 0)
            {
                _groundLayer.value = 1;
                builder.Append("\n");
                builder.Append($"Groundlayer��Nothing�ł��BLayer��Default�ɐݒ肵�܂����B");
            }

            if (_isDebug)
            {
                Debug.Log(builder.ToString());
            }
        }
    }
}