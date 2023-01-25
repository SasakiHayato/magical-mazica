using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextViewer : MonoBehaviour
{
    [System.Serializable]
    class TextData
    {
        [SerializeField] ControllerType _controllerType;
        [SerializeField] Text _text;

        public ControllerType Controller => _controllerType;
        public Text MSGText => _text;
    }

    [SerializeField] Text _targetText;
    [SerializeField] List<TextData> _textDataList = new List<TextData>();

    public string CurrentLog => _targetText.text;

    public void OnView()
    {
        foreach (TextData data in _textDataList)
        {
            if (data.Controller == InputSetting.CurrentController)
            {
                _targetText.text = data.MSGText.text;
                return;
            }
        }
    }
}
