using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEventTrigger : MonoBehaviour, IUIOperateEventable
{
    [System.Serializable]
    class TextViewData
    {
        [SerializeField] string _popupPath;
        [SerializeField] TutorialTextViewer _tutorialTextViewer;

        public string PopupPath => _popupPath;
        public TutorialTextViewer TextViewer => _tutorialTextViewer;
    }

    [SerializeField] int _maxSelectID = 1;
    [SerializeField] Text _logText;
    [SerializeField] List<TextViewData> _textViewDataList; 

    int _currentSelectID = 0;
    int _currentTextViewID = 0;
    bool _onCallback = false;

    Popup _popup;

    readonly string PlayerTag = "Player";

    void Start()
    {
        _popup = GUIManager.FindPopup(_textViewDataList[_currentTextViewID].PopupPath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag) && !_onCallback)
        {
            _onCallback = true;

            InputSetting.ChangeInputUser(InputUserType.UI);
            OnRequest();
        }
    }

    void OnRequest()
    {
        InputController.CallbackInputEvent(true);
        InputSetting.UIInputOperate.OperateRequest(this);
    }

    void IUIOperateEventable.OnEnableEvent()
    {
        if (_textViewDataList[_currentTextViewID].TextViewer != null)
        {
            _textViewDataList[_currentTextViewID].TextViewer.OnView();
        }
        
        _popup.OnView();
    }

    void IUIOperateEventable.Select(ref int horizontal, ref int vertical)
    {
        _currentSelectID = horizontal;

        if (0 > _currentSelectID)
        {
            _currentSelectID = 0;
        }

        if (_currentSelectID >= _maxSelectID)
        {
            _currentSelectID = _maxSelectID - 1;
        }

        _popup.OnSelect(_currentSelectID);

        horizontal = _currentSelectID;

        _popup.OnSelect(_currentSelectID);
    }

    bool IUIOperateEventable.SubmitEvent()
    {
        SoundManager.PlayRequest(SoundSystem.SoundType.SEInput, "Click");
        return true;
    }

    void IUIOperateEventable.CancelEvent()
    {
        //Dispose();   
    }

    void IUIOperateEventable.DisposeEvent()
    {
        _logText.text = _textViewDataList[_currentTextViewID].TextViewer.CurrentLog;
        Dispose();
    }

    void Dispose()
    {
        _popup.OnCancel();
        _currentSelectID = 0;

        if (_textViewDataList.Count - 1 == _currentTextViewID)
        {
            InputSetting.ChangeInputUser(InputUserType.Player);
            InputSetting.UIInputOperate.OperateRequest(null);
            InputController.CallbackInputEvent(false);
        }
        else
        {
            _currentTextViewID++;
            _popup = GUIManager.FindPopup(_textViewDataList[_currentTextViewID].PopupPath);
            OnRequest();
        }
    }
}
