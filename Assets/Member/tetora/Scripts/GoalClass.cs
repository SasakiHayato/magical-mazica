using UnityEngine;

public class GoalClass : MonoBehaviour, IUIOperateEventable
{
    int _currentSelectID;

    Popup _popup;

    readonly int MaxSelectID = 2;
    readonly int AttributeID = 0;
    readonly string PopupPath = "SelectGoal";

    void Start()
    {
        _popup = GUIManager.FindPopup(PopupPath);
    }

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
        _popup.OnView();
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

        _popup.OnSelect(_currentSelectID);
        
        horizontal = _currentSelectID;
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        _popup.OnSubmit();

        if (_currentSelectID == AttributeID)
        {
            // ‰¼‚ÌŠK‘w
            if (GameController.Instance.CurrentMapHierarchy > GameController.Instance.MaxMapHierarchy)
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
        _popup.OnCancel();
        _currentSelectID = 0;
        InputSetting.ChangeInputUser(InputUserType.Player);
        InputSetting.UIInputOperate.OperateRequest(null);
    }
}
