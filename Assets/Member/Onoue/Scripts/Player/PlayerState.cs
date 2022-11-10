using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoState.Player
{

    public class PlayerState : MonoBehaviour
    {
        public enum Player
        {
            Idle,
            Run,
            Attack
        }
        [SerializeField] NewPlayer _data;
        MonoStateAttribute<PlayerState> _stateMachine = new MonoStateAttribute<PlayerState>();

        private void Start()
        {
            void Start()
            {
                _stateMachine.Initalize(this);

                _data.TestData = "SampleData";
                _stateMachine
                    .SetData(_data);

                _stateMachine
                    .AddState(new PlayerIdle(), Player.Idle)
                    .AddState(new PlayerRun(), Player.Run)
                    .AddState(new PlayerAttack(), Player.Attack)
                    .SetRunRequest(Player.Idle);
            }
        }
    }
}
