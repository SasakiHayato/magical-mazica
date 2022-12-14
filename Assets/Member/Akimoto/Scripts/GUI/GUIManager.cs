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
    [SerializeField] List<Popup> _popupList;
    
    int IGameSetupable.Priority => 2;

    static List<Popup> s_popupList;

    void Awake()
    {
        s_popupList = _popupList;
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
    }

    public static Popup FindPopup(string path)
    {
        return s_popupList.Find(p => p.Path == path);
    }
}
