using UnityEngine;
using CustomPhysics.Data;

namespace CustomPhysics
{
    /// <summary>
    /// ���������̑���N���X
    /// </summary>

    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsOperator : MonoBehaviour
    {
        [SerializeField] GroundData _groundData;
        [SerializeField] GravityData _gravityData;

        bool _isForce;

        Vector2 _moveVelocity = Vector2.one;
        Vector2 _forceVelocity = Vector2.one;

        Rigidbody2D _rb;

        ForceModel _forceModel = new ForceModel();

        public bool IsGround { get; private set; }

        void Awake()
        {
            if (_groundData == null)
            {
                Debug.LogError("GrounData������܂���B");
            }

            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;

            _groundData.SetUp(transform);
        }

        void Update()
        {
            IsGround = _groundData.IsGround;   
        }

        void FixedUpdate()
        {
            if (IsGround)
            {
                _gravityData.Initalize();
            }

            _forceModel.Process();

            _rb.velocity = CollectVelocity();
        }

        Vector2 CollectVelocity()
        {
            Vector2 velocity = Vector2.Scale(_moveVelocity, _forceVelocity);
            return velocity;
        }

        /// <summary>
        /// ���������œ�����
        /// </summary>
        /// <param name="velocity">��������</param>
        /// <param name="attributeGravity">�d�͂��g�p���邩�ǂ���</param>
        public void Move(Vector2 velocity, bool attributeGravity = true)
        {
            Vector2 set;

            if (attributeGravity)
            {
                Vector2 gravity = _gravityData.Gravity;
                set = Vector2.Scale(velocity, gravity);

                if (set.y == 0)
                {
                    set.y = gravity.y * -1;
                }
            }
            else
            {
                set = velocity;
            }

            _moveVelocity = set;
        }

        /// <summary>
        ///�@�͂�������
        /// </summary>
        /// <param name="direction">�����Ƃ��̗�</param>
        /// <param name="forceType"></param>
        public void Force(Vector2 direction, ForceType forceType = ForceType.Impulse)
        {
            ForceData data = new ForceData(direction, forceType);
            _forceModel.SetData(data);
        }
    }
}