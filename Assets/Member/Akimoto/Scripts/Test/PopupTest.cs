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
        Debug.Log("Popup表示");
        await _guimanager.ActivePopup("テスト", "はい", () => Debug.Log("はい"), "いいえ", () => Debug.Log("いいえ"));
        Debug.Log("表示終わり");
        await _guimanager.ActivePopup("テスト", "はい", () => Debug.Log("はい"), "いいえ", () => Debug.Log("いいえ"));
        Debug.Log("二度目の表示終わり");
    }
}
