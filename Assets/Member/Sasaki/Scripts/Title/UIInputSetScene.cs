using UnityEngine;

public class UIInputSetScene : IUIOperateEventable, IGameSetupable
{
    int _currentID = 0;
    
    static Popup _popup;
    static PanelMover _panelMover = null;

    readonly string Path = "SetScene";
    readonly Vector2 MovePosition = new Vector2(-500, 0);

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
        InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
        _popup.OnCancel();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        _panelMover
            .SetCallbackAction(() =>
            {
                Callback();
                _popup.OnCancel();
            })
            .OnMove(_popup.Parent.position.Collect() + MovePosition);
    }

    void Callback()
    {
        switch (_currentID)
        {
            case 2:
                SoundManager.PlayRequest(SoundSystem.SoundType.SEPlayer, "Greeting");
                SceneViewer.SceneLoad(SceneViewer.SceneType.Game);
                break;
            case 1:
                SoundManager.PlayRequest(SoundSystem.SoundType.SEPlayer, "Greeting");
                SceneViewer.SceneLoad(SceneViewer.SceneType.Tutorial);
                break;
            case 0:
                InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
                break;
        }
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
