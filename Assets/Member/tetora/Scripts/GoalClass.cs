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
            Debug.Log("ゴールした");
            //マップを移動する関数をここで呼ぶ
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

        if(0 < _currentSelectID)
        {
            _currentSelectID = 0;
        }

        if (_currentSelectID >= MaxSelectID)
        {
            _currentSelectID = MaxSelectID - 1;
        }

        horizontal = _currentSelectID;
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        if (_currentSelectID == 1)
        {
            GameController.Instance.Dispose();
            GameController.Instance.Setup();
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
        GameController.Instance.UserInput.OperateRequest(this);
    }
}
