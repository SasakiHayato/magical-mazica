using System;
using UnityEngine;

public class Teleporter : MonoBehaviour, IUIOperateEventable
{
    [SerializeField] GameObject _point;
    [SerializeField] TeleportAttributer _teleportAttributer;
    [SerializeField] SelectButtonHelper _selectButtonHelper;

    int _id;
    int _currentSelectID;

    Action<int> _teleportEvent;
    Func<int, Transform> _getTeleport;

    public static bool OnSelect { get; private set; } = false;

    readonly string PlayerTag = "Player";

    void Start()
    {
        _point = Instantiate(_point);
        _point.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(PlayerTag)) return;

        InputSetting.UIInputOperate.IsOperateRequest = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_teleportAttributer.IsAttribute) return;

        if (collision.CompareTag(PlayerTag) && _selectButtonHelper)
        {
            _selectButtonHelper.HelpObj(true);
        }

        if (InputSetting.UIInputOperate.IsOperateRequest)
        {
            CameraOperator.CallEvent("SelectTeleport");
            InputSetting.ChangeInputUser(InputUserType.UI);
            InputSetting.UIInputOperate.OperateRequest(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag) && _selectButtonHelper)
        {
            _selectButtonHelper.HelpObj(false);
        }
    }

    public void SetData(int id, Action<int> teleportEvent, Func<int, Transform> getTeleportEvent)
    {
        _id = id;
        _teleportEvent = teleportEvent;
        _getTeleport = getTeleportEvent;
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        OnSelect = false;
        _point.SetActive(true);
        _currentSelectID = 0;
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        _currentSelectID = horizontal;

        if (_currentSelectID < 0)
        {
            _currentSelectID = 0;
        }

        if (_currentSelectID >= CreateMap.TepoatObjLength)
        {
            _currentSelectID = CreateMap.TepoatObjLength - 1;
        }

        _point.transform.position = _getTeleport.Invoke(_currentSelectID).position;
        horizontal = _currentSelectID;
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        if (_currentSelectID == _id)
        {
            return false;
        }

        _teleportEvent.Invoke(_currentSelectID);
        OnSelect = true;

        return true;
    }

    void IUIOperateEventable.CancelEvent()
    {
        OnSelect = true;
        Dispose();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        Dispose();
    }

    void Dispose()
    {
        InputSetting.ChangeInputUser(InputUserType.Player);
        InputSetting.UIInputOperate.OperateRequest(null);
        _point.SetActive(false);
    }
}
