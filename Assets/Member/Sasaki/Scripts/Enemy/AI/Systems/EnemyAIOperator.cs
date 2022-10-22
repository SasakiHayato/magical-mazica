using System;
using UnityEngine;

/// <summary>
/// ìGÇÃAIä«óùÉNÉâÉX
/// </summary>

public class EnemyAIOperator : MonoBehaviour
{
    [System.Serializable]
    class LayerData
    {
        [SerializeField] bool _isViewLine;
        [SerializeField] LayerMask _layer;
        [SerializeField] Vector2 _offset;
        [SerializeField] float _distance;

        public bool IsViewLine => _isViewLine;
        public LayerMask Layer => _layer;
        public Vector2 Offset => _offset;
        public float Distance => _distance;
    }

    [SerializeField] LayerData _groundLayerData;
    [SerializeField] LayerData _wallLayerData;
    [SerializeField] ExecuteData _exeucteData;

    int _rayCollect = 1;

    ExecuteProsseser _prosseser;

    public Vector2 MoveDir { get; private set; } 

    void Start()
    {
        _prosseser = new ExecuteProsseser(_exeucteData);
    }

    void Update()
    {
        Action action = _prosseser.OnNext(transform);

        if (IsGround())
        {
            action.Invoke();
            Vector2 move = _prosseser.MoveDir;
            move.x *= _rayCollect;
            MoveDir = move;
        }
        else
        {
            _rayCollect *= -1;
            MoveDir = Vector2.zero;
        }

        if (IsWall())
        {
            _rayCollect *= -1;
        }
    }

    bool IsGround()
    {
        // GroundLayerDataÇÃë„ì¸
        float distance = _groundLayerData.Distance;
        LayerMask layer = _groundLayerData.Layer;

        Vector2 direction = transform.up.Collect() * -1 + _groundLayerData.Offset;
        direction.x *= _rayCollect;

        return Physics2D.Raycast(transform.position, direction.normalized, distance, layer);
    }

    bool IsWall()
    {
        // WallLayerDataÇÃë„ì¸
        float distance = _wallLayerData.Distance;
        LayerMask layer = _wallLayerData.Layer;

        Vector2 right = transform.right.Collect();
        right *= _rayCollect;

        Vector2 direction = right + _wallLayerData.Offset;
        return Physics2D.Raycast(transform.position, direction.normalized, distance, layer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // GroundLayerÇÃï`âÊ
        if (_groundLayerData.IsViewLine)
        {
            float distance = _groundLayerData.Distance;
            Vector2 start = transform.position.Collect();
            Vector2 end = distance * transform.up.Collect() * -1 + transform.position.Collect();

            end.x *= _rayCollect;

            Gizmos.DrawLine(start, end + _groundLayerData.Offset);
        }

        // WallLayerÇÃï`âÊ
        if (_wallLayerData.IsViewLine)
        {
            float distance = _wallLayerData.Distance;
            Vector2 start = transform.position.Collect();
            Vector2 end = distance * transform.right.Collect() + transform.position.Collect();

            end.x *= _rayCollect;

            Gizmos.DrawLine(start, end + _wallLayerData.Offset);
        }
    }
}
