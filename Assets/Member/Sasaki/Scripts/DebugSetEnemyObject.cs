using UnityEngine;

// Note. 現状使用されているとんでもクラス。Debug用のはずだった
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
