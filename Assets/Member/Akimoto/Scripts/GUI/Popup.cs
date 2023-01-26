using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// Note. åªèÛIUIOperateEventableÇ∆ëgÇ›çáÇÌÇπÇ»Ç¢Ç∆Ç¢ÇØÇ»Ç¢ÅB
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

    Vector3 _initalizeScale = Vector3.zero;

    CanvasGroup _canvasGroup;

    public string Path => _popupPath;
    public int DataLength => _sourceDataList.Count;
    public RectTransform Parent => _parent;

    readonly float AnimDuration = 0.15f;

    void Awake()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;

        _initalizeScale = transform.localScale;
        transform.localScale = Vector3.zero;

        int id = 0;
        _sourceDataList.ForEach(s =>
        {
            s.Setup(id);
            id++;
        });

        _sourceDataList.ForEach(s => s.Initalize());
    }

    public void OnView()
    {
        SoundManager.PlayRequest(SoundSystem.SoundType.SEUI, "Popup");

        _canvasGroup.alpha = 1;
        gameObject.transform
            .DOScale(_initalizeScale, AnimDuration)
            .SetEase(Ease.Linear);
    }

    public void OnSelect(int id)
    {
        foreach (SourceData data in _sourceDataList)
        {
            float scale = data.ID == id ? _selectScaleRate : 1;
            data.View(scale);
        }
    }

    public void OnCancel()
    {
        _sourceDataList.ForEach(s => s.Initalize());
        SoundManager.PlayRequest(SoundSystem.SoundType.SEUI, "Popup");
        
        gameObject.transform
            .DOScale(Vector3.zero, AnimDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.alpha = 0);
    }
}
