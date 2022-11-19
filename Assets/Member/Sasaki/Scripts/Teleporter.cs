using System;
using UnityEngine;

public class Teleporter : MonoBehaviour, IUIOperateEventable
{
    int _id;
    int _currentSelectID;

    Action<int> _teleportEvent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameController.Instance.UserInput.IsOperateRequest)
        {
            GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.UserInterface);
            GameController.Instance.UserInput.OperateRequest(this);
        }
    }

    public void SetData(int id, Action<int> teleportEvent)
    {
        _id = id;
        _teleportEvent = teleportEvent;
    }

    void IUIOperateEventable.OnEnableEvent()
    {
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

        horizontal = _currentSelectID;
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        
        if (_currentSelectID == _id)
        {
            return false;
        }
        
        _teleportEvent.Invoke(_currentSelectID);

        return true;
    }

    void IUIOperateEventable.CancelEvent()
    {
        GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.Player);
        GameController.Instance.UserInput.OperateRequest(null);
    }

    void IUIOperateEventable.DisposeEvent()
    {
        GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.Player);
        GameController.Instance.UserInput.OperateRequest(null);
    }
}
