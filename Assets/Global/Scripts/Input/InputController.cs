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

    void Start()
    {
        if (_player != null)
        {
            SetupPlayerInput();
        }

        SetupUIInput();
    }

    void OnDestroy()
    {
        InputSetting.Dispose();
    }

    void SetupPlayerInput()
    {
        _inputSetting.CreateButtonInput("RightTrigger_2", () => _player.Attack(), InputUserType.Player);
        _inputSetting.CreateButtonInput("RightTrigger_1", () => _player.Fire(), InputUserType.Player);
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
    }

    void SetupUIInput()
    {
        _inputSetting.CreateButtonInput("WestButton", () => Submit(), InputUserType.UI);
        _inputSetting.CreateButtonInput("SouthButton", () => Cancel(), InputUserType.UI);
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.UI, dir => Select(dir));
    }

    void Select(Vector2 value)
    {
        _onSelectX = 0 == value.x ? false : _onSelectX;
        _onSelectY = 0 == value.y ? false : _onSelectY;

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

        _selectX = _selectX < 0 ? 0 : _selectX;
        _selectY = _selectY < 0 ? 0 : _selectY;

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