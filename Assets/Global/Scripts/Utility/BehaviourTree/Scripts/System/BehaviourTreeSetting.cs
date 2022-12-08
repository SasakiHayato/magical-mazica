using UnityEngine;
using BehaviourTree.Data;
using BehaviourTree.IO;

namespace BehaviourTree
{
    /// <summary>
    /// �Q�[���J�n�ƏI����BehaviourTree�̏�����
    /// </summary>
    
    public class BehaviourTreeSetting : MonoBehaviour
    {
        void Awake()
        {
            #if UNITY_EDITOR
            BehaviourTreeIO.Initialize();
            #endif
        }

        void OnDestroy()
        {
            #if UNITY_EDITOR
            BehaviourTreeIO.Update();
            #endif

            BehaviourTreeMasterData.Dispose();
        }
    }
}