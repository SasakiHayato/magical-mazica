using UnityEngine;
using UnityEngine.UI;

namespace MonoState.Sample
{
    public class StateUser : MonoBehaviour
    {
        public enum State
        { 
            Idle,
            Move,
            Run
        }

        [SerializeField] Button _buttonIdle;
        [SerializeField] Button _buttonMove;
        [SerializeField] Button _buttonRun;
        [SerializeField] SampleData _sampleData;

        MonoStateMachine<StateUser> _stateMachine = new MonoStateMachine<StateUser>();

        void Start()
        {
            _stateMachine.Initalize(this);

            _sampleData.Data = "SampleData";
            _stateMachine
                .SetData(_sampleData);

            _stateMachine
                .AddState(new Idle(), State.Idle)
                .AddState(new Move(), State.Move)
                .AddState(new Run(), State.Run)
                .SetRunRequest(State.Idle);

            

            _buttonIdle.onClick.AddListener(() => _stateMachine.ChangeState(State.Idle.ToString()));
            _buttonMove.onClick.AddListener(() => _stateMachine.ChangeState(State.Move.ToString()));
            _buttonRun.onClick.AddListener(() => _stateMachine.ChangeState(State.Run.ToString()));
        }
    }
}
