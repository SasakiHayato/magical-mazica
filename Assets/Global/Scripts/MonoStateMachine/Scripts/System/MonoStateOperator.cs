using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoState.Opration
{
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
