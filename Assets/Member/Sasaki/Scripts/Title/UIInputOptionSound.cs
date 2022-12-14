public class UIInputOptionSound : IUIOperateEventable
{
    enum UISelectType
    {
        Back = 0,
        MasterVolume = 1,
        BGMVolume = 2,
        SEVolume = 3,
    }

    int _currentID = 0;
    Popup _popup = null;

    readonly string Path = "OptionSound";

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
        InputSetting.UIInputOperate.OperateRequest(new UIInputOption());
        _popup.OnCancel();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        UISelectType type = (UISelectType)System.Enum.ToObject(typeof(UISelectType), _currentID);

        switch (type)
        {
            case UISelectType.Back:
                InputSetting.UIInputOperate.OperateRequest(new UIInputOption());
                break;
            case UISelectType.MasterVolume:
                break;
            case UISelectType.BGMVolume:
                break;
            case UISelectType.SEVolume:
                break;
        }

        _popup.OnCancel();
    }
}
