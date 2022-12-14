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
        _inputSetting.CreateButtonInput("Fire1", () => _player.Attack(), InputUserType.Player);
        _inputSetting.CreateButtonInput("Fire2", () => _player.Fusion(), InputUserType.Player);
        _inputSetting.CreateButtonInput("Jump", () => _player.Jump(), InputUserType.Player);
        _inputSetting.CreateButtonInput
            (
                "Submit", 
                () => InputSetting.UIInputOperate.IsOperateRequest = true, 
                InputUserType.Player
            );
        
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.Player, dir => _player.SetMoveDirection(new Vector2(dir.x, 0)));
        _inputSetting.CreateAxisInput("Horizontal2", "Vertical2", InputUserType.Player, dir => SetMaterial(dir));

        _inputSetting.CreateButtonInput("Submit", () => Submit(), InputUserType.UI);
        _inputSetting.CreateButtonInput("Cancel", () => Cancel(), InputUserType.UI);
        _inputSetting.CreateAxisInput("Horizontal", "Vertical", InputUserType.UI, dir => Select(dir));
    }

    private void OnDisable()
    {
        InputSetting.Dispose();
    }
    
    private void SetMaterial(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            _player.SetMaterialID(RawMaterialID.BombBean);
        }
        else if (dir == Vector2.down)
        {
            //ëfçﬁÇí«â¡Ç∑ÇÈÇ‹Ç≈ãÛÇì¸ÇÍÇÈ
            _player.SetMaterialID(RawMaterialID.Empty);
        }
        else if (dir == Vector2.left)
        {
            _player.SetMaterialID(RawMaterialID.PowerPlant);
        }
        else if (dir == Vector2.right)
        {
            //ëfçﬁÇí«â¡Ç∑ÇÈÇ‹Ç≈ãÛÇì¸ÇÍÇÈ
            _player.SetMaterialID(RawMaterialID.Empty);
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