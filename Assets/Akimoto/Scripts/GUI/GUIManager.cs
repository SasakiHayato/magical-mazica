using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UIManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] PlayerStatusPanel _playerStatusPanel;

    public void Setup()
    {
        //Playerが生成されたらPlayerの情報をPlayerのステータスを表示するクラスに渡す
        _characterManager.PlayerSpawn
            .Subscribe(p => _playerStatusPanel.SetSlider(p))
            .AddTo(_characterManager);
    }
}
