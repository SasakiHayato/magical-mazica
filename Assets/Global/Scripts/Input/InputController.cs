using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour
{
    int _selectX = 0;
    int _selectY = 0;
    bool _onSelectX = false;
    bool _onSelectY = false;

    float _timer = 0;
    readonly float ErrorCommandTime = 1f;

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

        _inputSetting.CreateButtonInput("Error_Command", () => _timer = 0, InputUserType.Player);
        _inputSetting.CreateButtonInput("Error_Command", () => SetupErrorCommand(), InputUserType.Player, InputType.Stay);

        Debug.Log(InputSetting.CurrentController);
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
        _inputSetting.CreateButtonInput("LeftTrigger_1", () => _player.Dodge(), InputUserType.Player);
        _inputSetting.CreateButtonInput
            (
                "SouthButton", 
                () => 
                {
                    if (InputSetting.UIInputOperate.IsInputAttribute)
                    {
                        InputSetting.UIInputOperate.IsOperateRequest = true;
                    }
                    else
                    {
                        _player.SetMaterialID(RawMaterialID.Penetration);
                    }
                }, 
                InputUserType.Player
            );
        _inputSetting.CreateButtonInput("EastButton", () => _player.SetMaterialID(RawMaterialID.PowerPlant), InputUserType.Player);
        _inputSetting.CreateButtonInput("WestButton", () => _player.SetMaterialID(RawMaterialID.Poison), InputUserType.Player);
        _inputSetting.CreateButtonInput("NorthButton", () => _player.SetMaterialID(RawMaterialID.BombBean), InputUserType.Player);
        
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.Player, dir => _player.SetMoveDirection(new Vector2(dir.x, 0)));
    }

    void SetupUIInput()
    {
        //if (InputSetting.CurrentController == ControllerType.None)
        //{
        //    _inputSetting.CreateButtonInput("Keybord_Submit", () => Submit(), InputUserType.UI);
        //}
        //else
        //{
        //    _inputSetting.CreateButtonInput("SouthButton", () => Submit(), InputUserType.UI);
        //}

        _inputSetting.CreateButtonInput("SouthButton", () => Submit(), InputUserType.UI);

        _inputSetting.CreateButtonInput("EastButton", () => Cancel(), InputUserType.UI);
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.UI, dir => Select(dir));
    }

    void SetupErrorCommand()
    {
        _timer += Time.deltaTime;

        if (_timer > ErrorCommandTime)
        {
            _timer = 0;
            SceneViewer.SceneLoad(SceneViewer.SceneType.Title, true);
        }
    }

    void Select(Vector2 value)
    {
        if (InputSetting.CurrentController != ControllerType.None)
        {
            value.y = value.y * -1;
        }

        _onSelectX = 0 == value.x ? false : _onSelectX;
        _onSelectY = 0 == value.y ? false : _onSelectY;

        if (0 != value.x && !_onSelectX)
        {
            _onSelectX = true;
            _selectX += (int)Mathf.Sign(value.x);
            SoundManager.PlayRequest(SoundSystem.SoundType.SEUI, "Cursor");
        }

        if (0 != value.y && !_onSelectY)
        {
            _onSelectY = true;
            _selectY += (int)Mathf.Sign(value.y);
            SoundManager.PlayRequest(SoundSystem.SoundType.SEUI, "Cursor");
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

    public static void CallbackInputEvent(bool onEvent)
    {
        List<IFieldObjectDatable> list = GameController.Instance.GetFieldObjectDatable(ObjectType.Enemy).ToList();
        IFieldObjectDatable player = GameController.Instance.GetFieldObjectDatable(ObjectType.Player).FirstOrDefault();

        if (player != null) list.Add(player);

        foreach (IFieldObjectDatable datable in list)
        {
            if (datable.Target.TryGetComponent(out IInputEventable input))
            {
                GameController.Instance.OnInputEvent = onEvent;
                if (onEvent) input.OnEvent();
                else input.DisposeEvent();
            }
        }
    }
}