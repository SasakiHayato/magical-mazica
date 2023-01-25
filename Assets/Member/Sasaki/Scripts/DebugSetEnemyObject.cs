using UnityEngine;

// Note. ����g�p����Ă���Ƃ�ł��N���X�BDebug�p�̂͂�������
public class DebugSetEnemyObject : MonoBehaviour
{
    [SerializeField] Transform _parent;

    static DebugSetEnemyObject Instance;

    void Awake()
    {
        Instance = this;
    }

    public static void SetEnemy(Transform target)
    {
        if (Instance == null) return; 
        
        target.SetParent(Instance._parent);
    }
}
