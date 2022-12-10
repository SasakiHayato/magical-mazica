using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UIManagement;

public class GUIManager : MonoBehaviour, IGameSetupable
{
    [SerializeField] FieldManager _fieldManager;
    [SerializeField] CharacterManager _characterManager;
    [SerializeField] RawMaterialData _materialData;
    [SerializeField] PlayerStatusPanel _playerStatusPanel;
    [SerializeField] Popup _popup;
    private static Popup _popupInstance;
    int IGameSetupable.Priority => 2;

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }

    void IGameSetupable.GameSetup()
    {
        //画面に表示する素材画像を設定
        if (_fieldManager)
        {
            _fieldManager.MaterialList
            .Subscribe(items =>
            {
                List<RawMaterialDatabase> materialDatas = new List<RawMaterialDatabase>();
                items.ForEach(item => materialDatas.Add(_materialData.GetMaterialData(item)));
                _playerStatusPanel.SetMaterialSprite(materialDatas);
            })
            .AddTo(_fieldManager);
        }

        //Playerが生成されたらPlayerの情報をPlayerのステータスを表示するクラスに渡す
        if (_characterManager)
        {
            _characterManager.PlayerSpawn
            .Subscribe(p =>
            {
                _playerStatusPanel.SetSlider(p);
                _playerStatusPanel.SetMaterialViewPanel(p);
            })
            .AddTo(_characterManager);
        }

        _popup.Setup(this);
        _popup.SetActive = false;
        _popupInstance = _popup;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>
    /// <param name="value">表示するテキスト</param>
    /// <param name="positiveEvent">左側のボタンが押された時の処理</param>
    /// <param name="negativeEvent">右側のボタンが押された時の処理</param>
    /// <returns></returns>
    public async UniTask ActivePopup(string value, string positiveTextValue, System.Action positiveEvent, string negativeTextValue, System.Action negativeEvent)
    {
        _popup.SetActive = true;
        await _popup.Active(value, positiveTextValue, positiveEvent, negativeTextValue, negativeEvent);
        _popup.SetActive = false;
    }
}
