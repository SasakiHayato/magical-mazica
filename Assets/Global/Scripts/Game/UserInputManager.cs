public interface IUIOperateEventable
{
    void OnEnableEvent();
    void Select(ref int horizontal, ref int vertical);
    bool SubmitEvent();
    void CancelEvent();
    void DisposeEvent();
}

public class UserInputManager
{
    public enum InputType
    {
        Player,
        UserInterface,
    }

    
    PlayerInputController _playerInput;

    public bool IsOperateRequest { get; set; }
    public InputType CurrentInputType { get; private set; }
    public IUIOperateEventable Operate { get; private set; }

    public void SetInput(InputType inputType)
    {
        //CurrentInputType = inputType;
        _playerInput.ChangeInput(inputType);
    }

    public void OperateRequest(IUIOperateEventable operate)
    {
        operate?.OnEnableEvent();
        Operate = operate;
    }

    public void SetController(PlayerInputController controller)
    {
        _playerInput = controller;
    }
}
