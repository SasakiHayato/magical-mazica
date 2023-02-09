public class UIInputBackTitle : IUIOperateEventable, IGameSetupable
{
    Popup _popup = null; 

    readonly string Path = "BackTitle";

    int IGameSetupable.Priority => 1;

    void IGameSetupable.GameSetup()
    {
        if (_popup == null)
        {
            _popup = GUIManager.FindPopup(Path);
        }

        InputSetting.UIInputOperate.OperateRequest(this);

        _popup.OnView();
    }

    void IUIOperateEventable.CancelEvent()
    {
        
    }

    void IUIOperateEventable.DisposeEvent()
    {
        SceneViewer.SceneLoad(SceneViewer.SceneType.Title, true);
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        SoundManager.PlayRequest(SoundSystem.SoundType.SEInput, "Click");
        return true;
    }
}
