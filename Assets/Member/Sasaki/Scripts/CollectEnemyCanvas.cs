using UnityEngine;

public class CollectEnemyCanvas : MonoBehaviour
{
    [SerializeField] Transform _root;

    Vector2 _scale;

    void Start()
    {
        _scale = transform.localScale;
    }

    void Update()
    {
        int x = (int)Mathf.Sign(_root.localScale.x);
        transform.localScale = new Vector2(_scale.x * x, _scale.y);
    }
}
