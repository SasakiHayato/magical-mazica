public class UIInputSelectTitle : IUIOperateEventable
{
    int _currentID = 0;

    static Popup _popup = null;
    static PanelMover _panelMover = null;

    readonly int AttributeID = 0;
    readonly string Path = "SelectTitle";
    readonly UnityEngine.Vector2 MovePosition = new UnityEngine.Vector2(500, 0);

    void IUIOperateEventable.OnEnableEvent()
    {
        if (_popup == null)
        {
            _popup = GUIManager.FindPopup(Path);
            _panelMover = new PanelMover(_popup.Parent, _popup.Parent.position);
        }

        _panelMover.Initalize();
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
        _panelMover
            .SetCallbackAction(() =>
            {
                if (_currentID == AttributeID) InputSetting.UIInputOperate.OperateRequest(new UIInputSetScene());
                else InputSetting.UIInputOperate.OperateRequest(new UIInputOption());
                
                _popup.OnCancel();
            })
            .OnMove(_popup.Parent.position.Collect() + MovePosition);
    }
}
