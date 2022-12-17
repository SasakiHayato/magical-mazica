using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [System.Serializable]
    class SourceData
    {
        [SerializeField] Image _source;

        Vector2 _offsetScale;

        public int ID { get; private set; }

        public void Setup(int id)
        {
            ID = id;
            _offsetScale = _source.transform.localScale;
        }

        public void View(float scaleRate)
        {
            _source.transform.localScale = _offsetScale * scaleRate;
        }

        public void Initalize()
        {
            _source.transform.localScale = _offsetScale;
        }
    }

    [SerializeField] string _popupPath;
    [SerializeField] float _selectScaleRate;
    [SerializeField] RectTransform _parent;
    [SerializeField] List<SourceData> _sourceDataList; 
    
    CanvasGroup _canvasGroup;

    public string Path => _popupPath;
    public int DataLength => _sourceDataList.Count;
    public RectTransform Parent => _parent;

    void Awake()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();

        int id = 0;
        _sourceDataList.ForEach(s =>
        {
            s.Setup(id);
            id++;
        });

        OnCancel();
    }

    public void OnView()
    {
        _canvasGroup.alpha = 1;
    }

    public void OnSelect(int id)
    {
        foreach (SourceData data in _sourceDataList)
        {
            float scale = data.ID == id ? _selectScaleRate : 1;
            data.View(scale);
        }
    }

    public void OnSubmit()
    {

    }

    public void OnCancel()
    {
        _sourceDataList.ForEach(s => s.Initalize());
        _canvasGroup.alpha = 0;
    }
}
