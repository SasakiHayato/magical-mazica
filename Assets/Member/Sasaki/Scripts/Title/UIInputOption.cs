public class UIInputOption : IUIOperateEventable
{
    enum SelectUIType
    {
        Back = 0,
        Sound = 1,
    }

    int _currentID = 0;
    Popup _popup;

    readonly string Path = "Option";

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
        SelectUIType type = (SelectUIType)System.Enum.ToObject(typeof(SelectUIType), _currentID);

        switch (type)
        {
            case SelectUIType.Sound:
                InputSetting.UIInputOperate.OperateRequest(new UIInputOptionSound());
                break;
            case SelectUIType.Back:
                InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
                break;
        }

        _popup.OnCancel();
    }
}
