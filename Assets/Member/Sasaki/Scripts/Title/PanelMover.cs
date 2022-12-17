using UnityEngine;
using DG.Tweening;

public class PanelMover
{
    Vector2 _offsetPosition = Vector2.zero;

    System.Action _callback = null;
    RectTransform _rect;

    public PanelMover(RectTransform rect, Vector2 offsetPosition)
    {
        _rect = rect;
        _offsetPosition = offsetPosition;
    }

    public PanelMover SetCallbackAction(System.Action action)
    {
        _callback = action;
        _callback += () => _callback = null;

        return this;
    }

    public void OnMove(Vector2 setPosition, float duration = 0.5f, Ease ease = Ease.Linear)
    {
        _rect
            .DOMove(setPosition, duration)
            .SetEase(ease)
            .OnComplete(() => _callback?.Invoke());
    }

    public void Initalize(float duration = 0.5f, Ease ease = Ease.Linear)
    {
        _rect
            .DOMove(_offsetPosition, duration)
            .SetEase(ease);
    }
}
