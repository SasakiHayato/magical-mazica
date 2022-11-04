using UnityEngine;
using System;

namespace MonoState
{
    public partial class StateOperator : MonoBehaviour
    {
        Action _action;
       
        public void Setup(Action action)
        {
            _action = action;
        }

        void Update()
        {
            _action.Invoke();
        }

        void OnDestroy()
        {
            Destroy(gameObject.GetComponent<StateOperator>());
        }
    }
}
