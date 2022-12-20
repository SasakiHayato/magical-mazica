using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SelectButtonHelper : MonoBehaviour
{
    [SerializeField] GameObject _xboxHelpObj;
    [SerializeField] GameObject _ps4HelpObj;

    private void Start()
    {
        _xboxHelpObj.SetActive(false);
        _ps4HelpObj.SetActive(false);
    }

    public void HelpObj(bool active)
    {
        switch (InputSetting.CurrentController)
        {
            case ControllerType.XBOX:
                _xboxHelpObj.SetActive(active);
                break;
            case ControllerType.DUALSHOCK:
                _ps4HelpObj.SetActive(active);
                break;
            case ControllerType.None:
                _xboxHelpObj.SetActive(false);
                _ps4HelpObj.SetActive(false);
                break;
            default:
                break;
        }
    }
}
