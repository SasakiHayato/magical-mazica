using UnityEngine;

public class InputController : MonoBehaviour
{
    int _selectX = 0;
    int _selectY = 0;
    bool _onSelectX = false;
    bool _onSelectY = false;

    Player _player;

    InputSetting _inputSetting;

    private void Awake()
    {
        TryGetComponent(out _player);
        InputSetting.SetInputUser(gameObject, out _inputSetting);
    }

    void OnEnable()
    {
        _inputSetting.CreateButtonInput("RightTrigger_2", () => _player.Attack(), InputUserType.Player);
        _inputSetting.CreateButtonInput("RightTrigger_1", () => _player.Fusion(), InputUserType.Player);
        _inputSetting.CreateButtonInput("LeftTrigger_2", () => _player.Jump(), InputUserType.Player);
        _inputSetting.CreateButtonInput("WestButton", () => _player.SetMaterialID(RawMaterialID.Empty), InputUserType.Player);
        _inputSetting.CreateButtonInput("EastButton", () => _player.SetMaterialID(RawMaterialID.PowerPlant), InputUserType.Player);
        _inputSetting.CreateButtonInput("SouthButton", () => _player.SetMaterialID(RawMaterialID.Empty), InputUserType.Player);
        _inputSetting.CreateButtonInput("NorthButton", () => _player.SetMaterialID(RawMaterialID.BombBean), InputUserType.Player);
        _inputSetting.CreateButtonInput
            (
                "LeftStickButton", 
                () => InputSetting.UIInputOperate.IsOperateRequest = true, 
                InputUserType.Player
            );
        
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.Player, dir => _player.SetMoveDirection(new Vector2(dir.x, 0)));
        
        _inputSetting.CreateButtonInput("WestButton", () => Submit(), InputUserType.UI);
        _inputSetting.CreateButtonInput("SouthButton", () => Cancel(), InputUserType.UI);
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.UI, dir => Select(dir));
    }

    private void OnDisable()
    {
        InputSetting.Dispose();
    }
    
    void Select(Vector2 value)
    {
        if (0 == value.x)
        {
            _onSelectX = false;
        }

        if (0 == value.y)
        {
            _onSelectY = false;
        }

        if (0 != value.x && !_onSelectX)
        {
            _onSelectX = true;
            _selectX += (int)Mathf.Sign(value.x);
        }

        if (0 != value.y && !_onSelectY)
        {
            _onSelectY = true;
            _selectY += (int)Mathf.Sign(value.y);
        }

        if (_selectX < 0)
        {
            _selectX = 0;
        }

        if (_selectY < 0)
        {
            _selectY = 0;
        }

        InputSetting.UIInputOperate.Operate.Select(ref _selectX, ref _selectY);
    }

    void Submit()
    {
        if (InputSetting.UIInputOperate.Operate == null) return;

        if (InputSetting.UIInputOperate.Operate.SubmitEvent())
        {
            InputSetting.UIInputOperate.Operate.DisposeEvent();
        }
    }

    void Cancel()
    {
        if (InputSetting.UIInputOperate.Operate == null) return;

        InputSetting.UIInputOperate?.Operate.CancelEvent();
        _selectX = 0;
        _selectY = 0;
    }
}