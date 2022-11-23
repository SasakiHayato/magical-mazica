using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalClass : MonoBehaviour, IUIOperateEventable
{

    int _currentSelectID;

    readonly int MaxSelectID = 2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameController.Instance.UserInput.IsOperateRequest)
        {
            GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.UserInterface);
            GameController.Instance.UserInput.OperateRequest(this);
        }
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        _currentSelectID = 0;
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        _currentSelectID = horizontal;

        if(0 > _currentSelectID)
        {
            _currentSelectID = 0;
        }

        if (_currentSelectID >= MaxSelectID)
        {
            _currentSelectID = MaxSelectID - 1;
        }

        Debug.Log($"OnNext => {_currentSelectID == 1} ID {_currentSelectID}");

        horizontal = _currentSelectID;
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        if (_currentSelectID == 1)
        {
            SceneViewer.Initalize();
        }

        return true;
    }

    void IUIOperateEventable.CancelEvent()
    {
        GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.Player);
        GameController.Instance.UserInput.OperateRequest(this);
    }

    void IUIOperateEventable.DisposeEvent()
    {
        GameController.Instance.UserInput.ChangeInput(UserInputManager.InputType.Player);
        GameController.Instance.UserInput.OperateRequest(null);
    }
}
