using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldTouchOperator : MonoBehaviour
{
    public enum TouchType
    {
        Ground,
        Wall,
    }

    public enum DirectionType
    {
        Right,
        Down,
    }

    [System.Serializable]
    class LayerData
    {
        [SerializeField] TouchType _touchType;
        [SerializeField] Vector2 _offset;
        [SerializeField] LayerMask _touchLayer;
        [SerializeField] DirectionType _directionType;
        [SerializeField] float _distance;
        [SerializeField] bool _onGizmo;

        public TouchType TouchType => _touchType;
        public Vector2 Offset => _offset;
        public LayerMask TouchLayer => _touchLayer;
        public DirectionType DirectionType => _directionType;
        public float Distance => Mathf.Abs(_distance);
        public bool OnGizmo => _onGizmo;
    }

    [SerializeField] List<LayerData> _layerDataList;

    int _dirCollect = 1;

    public bool IsTouch(TouchType type, bool any = false)
    {
        var list = _layerDataList.Where(d => d.TouchType == type);

        return !any ? list.All(d => OnProcess(d)) : list.Any(d => OnProcess(d)); ;
    }

    public bool IsTouchAll()
    {
        return _layerDataList.All(d => OnProcess(d));
    }

    public void ChangeDirCollect()
    {
        _dirCollect *= -1;
    }

    bool OnProcess(LayerData d)
    {
        return Physics2D.Raycast(transform.position, SetDir(d), d.Distance, d.TouchLayer);
    }

    Vector2 SetDir(LayerData d)
    {
        Vector2 set = Vector2.zero;

        switch (d.DirectionType)
        {
            case DirectionType.Right: set = transform.right;
                break;
            case DirectionType.Down: set = transform.up * -1;
                break;
        }

        Vector2 dir = set + d.Offset;
        dir.x *= _dirCollect;

        return dir;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var list = _layerDataList.Where(d => d.OnGizmo);
        foreach (LayerData d in list)
        {
            Ray ray = new Ray(transform.position, SetDir(d));
            Gizmos.DrawLine(transform.position, ray.GetPoint(d.Distance));
        }
    }
}
