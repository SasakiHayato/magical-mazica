using System.Collections.Generic;
using UnityEngine;

namespace EnemyAISystem
{
    public interface IAttack
    {
        int SendAttackIndex();
    }

    [System.Serializable]
    public class EnemyAIAttackData
    {
        [System.Serializable]
        public class AttackData
        {
            [SerializeField] string _animStateName;
            [SerializeField] EnemyAttackCollider _collider;
            [SerializeField] int _isActiveFrame;
            [SerializeField] int _endActivrFrame;
            [SerializeField] int _waitExitFame;

            public string AnimStateName => _animStateName; 
            public EnemyAttackCollider Collider => _collider;
            public int IsActiveFrame => _isActiveFrame;
            public int EndActiveFrame => _endActivrFrame;
            public int WaitExitFrame => _waitExitFame;
        }

        [SerializeField] List<AttackData> _attackDataList;

        public AttackData GetAttackData(int index) => _attackDataList[index];
    }
}
