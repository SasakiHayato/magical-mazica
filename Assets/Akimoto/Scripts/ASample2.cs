using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class ASample2 : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Toggle _toggle;
    private ReactiveCommand _command = new ReactiveCommand();

    private void Start()
    {
        _command = _toggle.OnValueChangedAsObservable().ToReactiveCommand();
        _command.BindTo(_button);

        _button
            .OnClickAsObservable()
            .Subscribe(_ => Debug.Log("‰Ÿ‚³‚ê‚½"));
    }
}
