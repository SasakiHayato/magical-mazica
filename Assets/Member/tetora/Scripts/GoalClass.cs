using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalClass : MonoBehaviour, IUIOperateEventable
{
    int _currentSelectID;

    readonly int MaxSelectID = 2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && InputSetting.UIInputOperate.IsOperateRequest)
        {
            InputSetting.ChangeInputUser(InputUserType.UI);
            InputSetting.UIInputOperate.OperateRequest(this);
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
            // ‰¼‚ÌŠK‘w
            if (GameController.Instance.CurrentMapHierarchy > 2)
            {
                SceneViewer.SceneLoad(SceneViewer.SceneType.Boss);
            }
            else
            {
                GameController.Instance.AddMapHierarchy();
                SceneViewer.Initalize();
            }
        }

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
        InputSetting.ChangeInputUser(InputUserType.Player);
        InputSetting.UIInputOperate.OperateRequest(null);
    }
}
