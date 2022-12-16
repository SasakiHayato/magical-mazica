using UnityEngine;

public class UIInputOptionSound : IUIOperateEventable, IGameSetupable
{
    enum UISelectType
    {
        Back = 0,
        MasterVolume = 1,
        BGMVolume = 2,
        SEVolume = 3,
    }

    int _currentID = 0;
    
    static Popup _popup = null;
    static PanelMover _panelMover = null;

    readonly string Path = "OptionSound";
    readonly Vector2 MovePosition = new Vector2(500, 0);

    int IGameSetupable.Priority => 1;

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
        InputSetting.UIInputOperate.OperateRequest(new UIInputOption());
        _popup.OnCancel();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        UISelectType type = (UISelectType)System.Enum.ToObject(typeof(UISelectType), _currentID);

        _panelMover
            .SetCallbackAction(() =>
            {
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
            })
            .OnMove(_popup.Parent.position.Collect() + MovePosition);
    }

    void IGameSetupable.GameSetup()
    {
        if (_popup == null)
        {
            _popup = GUIManager.FindPopup(Path);
            _panelMover = new PanelMover(_popup.Parent, _popup.Parent.position);
        }

        _panelMover.OnMove(_popup.Parent.position.Collect() + MovePosition);
    }
}
