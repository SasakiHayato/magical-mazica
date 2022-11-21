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
        [SerializeField] string _path;
        [SerializeField] TouchType _touchType;
        [SerializeField] Vector2 _offset;
        [SerializeField] LayerMask _touchLayer;
        [SerializeField] DirectionType _directionType;
        [SerializeField] float _distance;
        [SerializeField] bool _onGizmo;

        public int ID { get; set; }
        public string Path => _path;
        public TouchType TouchType => _touchType;
        public Vector2 Offset => _offset;
        public LayerMask TouchLayer => _touchLayer;
        public DirectionType DirectionType => _directionType;
        public float Distance => Mathf.Abs(_distance);
        public bool OnGizmo => _onGizmo;
    }

    public struct LayerInfo
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public TouchType TouchType { get; set; }
        public LayerMask TouchLayer { get; set; }
    }

    [SerializeField] Transform _user;
    [SerializeField] List<LayerData> _layerDataList;

    int _dirCollect = 1;

    

    void Start()
    {
        int id = 0;
        _layerDataList.ForEach(l => 
        {
            l.ID = id;
            id++;
        });

        _dirCollect = (int)Mathf.Sign(_user.localScale.x);
    }

    public bool IsTouch(TouchType type, bool any = false)
    {
        var list = _layerDataList.Where(d => d.TouchType == type);

        return !any ? list.All(d => OnProcess(d)) : list.Any(d => OnProcess(d)); ;
    }

    public bool IsTouchAll()
    {
        return _layerDataList.All(d => OnProcess(d));
    }

    public void OnChangeLayDir()
    {
        _dirCollect *= -1;
    }

    public void IsTouchLayerID(out int[] idArray)
    {
        var list = _layerDataList.Where(l => OnProcess(l)).ToArray();
        idArray = new int[list.Count()];
        for (int index = 0; index < idArray.Length; index++)
        {
            idArray[index] = list[index].ID;
        }
    }

    public void IsTouchLayerPath(out string[] pathArray)
    {
        var list = _layerDataList.Where(l => OnProcess(l)).ToArray();
        pathArray = new string[list.Count()];
        for (int index = 0; index < pathArray.Length; index++)
        {
            pathArray[index] = list[index].Path;
        }
    }

    public LayerInfo GetLayerInfo(int id)
    {
        LayerData data = _layerDataList.FirstOrDefault(l => l.ID == id);
        LayerInfo info = new LayerInfo
        {
            ID = data.ID,
            Path = data.Path,
            TouchLayer = data.TouchLayer,
            TouchType = data.TouchType
        };

        return info;
    }

    public LayerInfo GetLayerInfo(string path)
    {
        LayerData data = _layerDataList.FirstOrDefault(l => l.Path == path);
        LayerInfo info = new LayerInfo
        {
            ID = data.ID,
            Path = data.Path,
            TouchLayer = data.TouchLayer,
            TouchType = data.TouchType
        };

        return info;
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
        if (_layerDataList == null) return;

        Gizmos.color = Color.red;
        var list = _layerDataList.Where(d => d.OnGizmo);
        foreach (LayerData d in list)
        {
            Ray ray = new Ray(transform.position, SetDir(d));
            Gizmos.DrawLine(transform.position, ray.GetPoint(d.Distance));
        }
    }
}
