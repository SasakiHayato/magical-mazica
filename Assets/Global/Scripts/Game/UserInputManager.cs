public interface IUIOperateEventable
{
    void Select(int horizontal, int vertical);
    void SubmitEvent();
    void CancelEvent();
}

public class UserInputManager
{
    public enum InputType
    {
        Player,
        UserInterface,
    }

    PlayerInputController _playerInput;

    public IUIOperateEventable Operate { get; private set; }

    public void ChangeInput(InputType inputType)
    {
        _playerInput.ChangeInput(inputType);
    }

    public void OperateRequest(IUIOperateEventable operate)
    {
        Operate = operate;
    }

    public void SetController(PlayerInputController controller)
    {
        _playerInput = controller;
    }
}
