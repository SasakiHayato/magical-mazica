using UnityEngine;

public class UIInputSetScene : IUIOperateEventable
{
    int _currentID = 0;
    Popup _popup;

    readonly int AttributeID = 1;
    readonly string Path = "SetScene";
    
    void IUIOperateEventable.OnEnableEvent()
    {
        if (_popup == null)
        {
            _popup = GUIManager.FindPopup(Path);
        }
        
        _popup.OnView();
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        if (_popup.DataLength <= vertical)
        {
            vertical = _popup.DataLength - 1;
        }
        _currentID = vertical;
        _popup.OnSelect(vertical);
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        SoundManager.PlayRequest(SoundSystem.SoundType.SEInput, "Click");
        return true;
    }

    void IUIOperateEventable.CancelEvent()
    {
        InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
        _popup.OnCancel();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        if (_currentID == AttributeID)
        {
            SoundManager.PlayRequest(SoundSystem.SoundType.SEInput, "Click");
            SceneViewer.SceneLoad(SceneViewer.SceneType.Game);
        }
        else
        {
            InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
        }

        _popup.OnCancel();
    }
}
