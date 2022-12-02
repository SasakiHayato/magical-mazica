using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    int _selectX = 0;
    int _selectY = 0;
    bool _onSelectX = false;
    bool _onSelectY = false;

    PlayerInput _playerInput;
    Player _player;
    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _player);
        GameController.Instance.UserInput.SetController(this);
    }
    void OnEnable()
    {
        _playerInput.actions["Jump"].started += ContextMenu => 
        {
            OnJump(ContextMenu);
        };
        _playerInput.actions["Attack"].started += OnAttack;
        _playerInput.actions["Fire"].started += OnFire;
        _playerInput.actions["Fusion"].started += OnFusion;
        _playerInput.actions["SetMaterial"].started += OnSet;

        _playerInput.actions["PlayerSubmit"].started +=
            context => GameController.Instance.UserInput.IsOperateRequest = true;
        _playerInput.actions["PlayerSubmit"].canceled +=
            context => GameController.Instance.UserInput.IsOperateRequest = false;

        _playerInput.actions["UISubmit"].started += context => Submit();
        _playerInput.actions["UICancel"].started += context => Cancel();
    }

    private void OnDisable()
    {
        _playerInput.actions["Jump"].started -= OnJump;
        _playerInput.actions["Attack"].started -= OnAttack;
        _playerInput.actions["Fire"].started -= OnFire;
        _playerInput.actions["Fusion"].started -= OnFusion;
        _playerInput.actions["SetMaterial"].started -= OnSet;

        _playerInput.actions["PlayerSubmit"].started -=
            context => GameController.Instance.UserInput.IsOperateRequest = true;
        _playerInput.actions["PlayerSubmit"].canceled -=
            context => GameController.Instance.UserInput.IsOperateRequest = false;

        _playerInput.actions["UISubmit"].started -= context => Submit();
        _playerInput.actions["UICancel"].started -= context => Cancel();
    }
    private void FixedUpdate()
    {
        if (_player != null)
        {
            var direction = _playerInput.actions["Move"].ReadValue<Vector2>().x;
            _player.Direction = new Vector2(direction, 0);
        }

        if (_playerInput.currentActionMap.name == UserInputManager.InputType.UserInterface.ToString()
            && GameController.Instance.UserInput.Operate != null)
        {
            Select(_playerInput.actions["UISelect"].ReadValue<Vector2>());
        }
    }
    
    private void OnJump(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        _player.Attack();
    }
    private void OnFire(InputAction.CallbackContext obj)
    {
        _player.Fire();
    }
    private void OnFusion(InputAction.CallbackContext obj)
    {
        _player.Fusion();
    }
    private void OnSet(InputAction.CallbackContext obj)
    {
        SetMaterial(obj);
    }
    private void SetMaterial(InputAction.CallbackContext obj)
    {
        var get = obj.action.actionMap["SetMaterial"].ReadValue<Vector2>();
        if (get == Vector2.up)
        {
            _player.SetMaterialID(RawMaterialID.BombBean);
        }
        else if (get == Vector2.down)
        {

        }
        else if (get == Vector2.left)
        {
            _player.SetMaterialID(RawMaterialID.PowerPlant);
        }
        else if (get == Vector2.right)
        {

        }
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

        GameController.Instance.UserInput.Operate.Select(ref _selectX, ref _selectY);
    }

    void Submit()
    {
        if (GameController.Instance.UserInput.Operate == null) return;

        if (GameController.Instance.UserInput.Operate.SubmitEvent())
        {
            GameController.Instance.UserInput.Operate.DisposeEvent();
        }
    }

    void Cancel()
    {
        if (GameController.Instance.UserInput.Operate == null) return;

        GameController.Instance.UserInput?.Operate.CancelEvent();
        _selectX = 0;
        _selectY = 0;
    }

    public void ChangeInput(UserInputManager.InputType inputType)
    {
        _playerInput.SwitchCurrentActionMap(inputType.ToString());
    }
}