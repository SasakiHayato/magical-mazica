using UnityEngine;
using MonoState.Data;

namespace MonoState.Sample
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SampleMonoStateUser : MonoBehaviour, IMonoDatableUni<SampleMonoStateUser>
    {
        public enum State
        {
            Idle,
            Move,
            Float,
        }

        [SerializeField] float _durationTime;
        [SerializeField] LayerMask _groundLayer;

        Rigidbody2D _rb;

        MonoStateMachine<SampleMonoStateUser> _stateMachine;
        SampleMonoStateData _sampleData = new SampleMonoStateData();

        SampleMonoStateUser IMonoDatableUni<SampleMonoStateUser>.GetData => this;
        string IMonoDatable.Path => nameof(SampleMonoStateUser);

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            _stateMachine = new MonoStateMachine<SampleMonoStateUser>(this, _durationTime);

            _stateMachine
                .AddState(State.Idle, new SampleStateIdle())
                .AddState(State.Move, new SampleStateMove())
                .AddState(State.Float, new SampleStateFloat());

            _stateMachine
                .AddMonoData(_sampleData)
                .AddMonoData(this);

            _stateMachine.IsRun = true;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _sampleData.OnJump = true;
            }
            
            _rb.velocity = _sampleData.MoveDir;
            Debug.Log(_stateMachine.CurrentKey);
        }

        public bool IsGround()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, _groundLayer);
        }
    }
}
