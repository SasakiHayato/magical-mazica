using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSetScene : MonoBehaviour, IUIOperateEventable
{
    [SerializeField] SceneViewer.SceneType _sceneType;
    
    void Start()
    {
        GameController.Instance.UserInput.OperateRequest(this);
    }

    void IUIOperateEventable.CancelEvent()
    {
        //throw new System.NotImplementedException();
    }

    void IUIOperateEventable.DisposeEvent()
    {
        //throw new System.NotImplementedException();
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        Debug.Log("aa");
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        //throw new System.NotImplementedException();
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        SceneViewer.SceneLoad(_sceneType);
        return true;
    }
}
