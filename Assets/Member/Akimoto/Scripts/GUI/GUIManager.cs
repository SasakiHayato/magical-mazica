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
    int IGameSetupable.Priority => 2;

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }

    void IGameSetupable.GameSetup()
    {
        //画面に表示する素材画像を設定
        _fieldManager.MaterialList
            .Subscribe(items =>
            {
                List<RawMaterialDatabase> materialDatas = new List<RawMaterialDatabase>();
                items.ForEach(item => materialDatas.Add(_materialData.GetMaterialData(item)));
                _playerStatusPanel.SetMaterialSprite(materialDatas);
            })
            .AddTo(_fieldManager);

        //Playerが生成されたらPlayerの情報をPlayerのステータスを表示するクラスに渡す
        _characterManager.PlayerSpawn
            .Subscribe(p =>
            {
                _playerStatusPanel.SetSlider(p);
                _playerStatusPanel.SetMaterialViewPanel(p);
            })
            .AddTo(_characterManager);
    }
}
