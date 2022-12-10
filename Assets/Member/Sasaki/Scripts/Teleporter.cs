using System;
using UnityEngine;

public class Teleporter : MonoBehaviour, IUIOperateEventable
{
    [SerializeField] GameObject _point;

    int _id;
    int _currentSelectID;

    Action<int> _teleportEvent;
    Func<int, Transform> _getTeleport;

    public static bool OnSelect { get; private set; } = false;

    void Start()
    {
        _point = Instantiate(_point);
        _point.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameController.Instance.UserInput.IsOperateRequest)
        {
            CameraOperator.CallEvent("SelectTeleport");
            GameController.Instance.UserInput.SetInput(UserInputManager.InputType.UserInterface);
            GameController.Instance.UserInput.OperateRequest(this);
        }
    }

    public void SetData(int id, Action<int> teleportEvent, Func<int, Transform> getTeleportEvent)
    {
        _id = id;
        _teleportEvent = teleportEvent;
        _getTeleport = getTeleportEvent;
        Debug.Log(_getTeleport);
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
        Dispose();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        Dispose();
    }

    void Dispose()
    {
        GameController.Instance.UserInput.SetInput(UserInputManager.InputType.Player);
        GameController.Instance.UserInput.OperateRequest(null);
        _point.SetActive(false);
    }
}
