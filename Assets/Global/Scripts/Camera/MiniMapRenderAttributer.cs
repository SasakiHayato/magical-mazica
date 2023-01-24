using UnityEngine;
using UniRx; 

public class MiniMapRenderAttributer : MonoBehaviour
{
    [SerializeField] GameObject _renderObject;

    GameObject _clone = null;
    ReactiveProperty<float> _reactiveDistance = new ReactiveProperty<float>();

    static readonly float AttributeDistance = 10;
    readonly string MiniMapLayer = "MiniMap";

    void Start()
    {
        CreateView();
        SetLayer();
        
        _clone.SetActive(false);
    }

    void Update()
    {
        if (GameController.Instance.Player == null || _clone.activeSelf) return;

        float dist = Vector2.Distance(GameController.Instance.Player.position, _renderObject.transform.position);
        if (dist < AttributeDistance)
        {
            _clone.SetActive(true);
        }
    }

    void CreateView()
    {
        _clone = Instantiate(_renderObject);
        _clone.name = "MiniMapView";
        _clone.transform.SetParent(transform);

        _clone.transform.position = _renderObject.transform.position;
        _clone.transform.localScale = _renderObject.transform.localScale;
    }

    void SetLayer()
    {
        _clone.layer = LayerMask.NameToLayer(MiniMapLayer);

        for (int index = 0; index < _clone.transform.childCount; index++)
        {
            _clone.transform.GetChild(index).gameObject.layer = LayerMask.NameToLayer(MiniMapLayer);
        }
    }
}
