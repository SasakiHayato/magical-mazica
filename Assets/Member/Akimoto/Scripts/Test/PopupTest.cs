using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PopupTest : MonoBehaviour
{
    [SerializeField] GUIManager _guimanager;

    private async void Start()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        Debug.Log("Popup�\��");
        await _guimanager.ActivePopup("�e�X�g", "�͂�", () => Debug.Log("�͂�"), "������", () => Debug.Log("������"));
        Debug.Log("�\���I���");
        await _guimanager.ActivePopup("�e�X�g", "�͂�", () => Debug.Log("�͂�"), "������", () => Debug.Log("������"));
        Debug.Log("��x�ڂ̕\���I���");
    }
}
