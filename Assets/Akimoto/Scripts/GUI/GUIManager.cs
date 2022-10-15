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
        //_characterManager.CurrentPlayer.HP.Subscribe(x => _playerStatusPanel.UpdateLifeSlider(x)).AddTo(_characterManager.CurrentPlayer);
    }
}
