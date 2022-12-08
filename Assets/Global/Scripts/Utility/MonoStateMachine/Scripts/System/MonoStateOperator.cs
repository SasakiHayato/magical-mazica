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
    /// ステートを走らせるためのクラス
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
