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
        //Player���������ꂽ��Player�̏���Player�̃X�e�[�^�X��\������N���X�ɓn��
        _characterManager.PlayerSpawn
            .Subscribe(p => _playerStatusPanel.SetSlider(p))
            .AddTo(_characterManager);
    }
}
