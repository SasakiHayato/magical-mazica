using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class ASample2 : MonoBehaviour
{
    [SerializeField] GUIManager _guiManager;
    [SerializeField] CharacterManager _characterManager;

    private void Start()
    {
        _guiManager.Setup();
        //_characterManager.CreatePlayer();
    }
}
