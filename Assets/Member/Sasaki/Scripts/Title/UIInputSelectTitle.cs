public class UIInputSelectTitle : IUIOperateEventable
{
    int _currentID = 0;
    Popup _popup = null;

    readonly int AttributeID = 1;
    readonly string Path = "SelectTitle";

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

    }

    void IUIOperateEventable.DisposeEvent()
    {
        if (_currentID == AttributeID)
        {
            InputSetting.UIInputOperate.OperateRequest(new UIInputSetScene());
        }
        else
        {
            InputSetting.UIInputOperate.OperateRequest(new UIInputOption());
        }

        _popup.OnCancel();
    }
}
