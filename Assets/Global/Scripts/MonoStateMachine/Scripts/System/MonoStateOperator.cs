using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoState.Opration
{
    public interface IStateExitEventable
    {
        void ExitEvent();
    }

    /// <summary>
    /// �X�e�[�g�𑖂点�邽�߂̃N���X
    /// </summary>
    public class MonoStateOperator : MonoBehaviour
    {
        public System.Action Run { get; set; }

        void Update()
        {
            Run.Invoke();
        }
    }
}
