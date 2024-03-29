using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UIManagement;

//public class TutorialUIManager : MonoBehaviour, IGameSetupable
//{
//    [SerializeField]
//    TutorialFieldManager _fieldManager;
//    [SerializeField]
//    CharacterManager _characterManager;
//    [SerializeField]
//    TaskManager _taskManager;
//    [SerializeField]
//    RawMaterialData _materialData;
//    [SerializeField]
//    PlayerStatusPanel _playerStatusPanel;
//    [SerializeField]
//    List<Popup> _popupList;
//    [SerializeField]
//    Text _taskText;


//    int IGameSetupable.Priority => 2;

//    static List<Popup> s_popupList;
//    public static TutorialUIManager Instance { get; private set; }

//    void Awake()
//    {
//        s_popupList = _popupList;
//        GameController.Instance.AddGameSetupable(this);
//        Instance = this;
//    }

//    void IGameSetupable.GameSetup()
//    {
//        //画面に表示する素材画像を設定
//        if (_fieldManager)
//        {
//            _fieldManager.MaterialList
//            .Subscribe(items =>
//            {
//                List<RawMaterialDatabase> materialDatas = new List<RawMaterialDatabase>();
//                items.ForEach(item => materialDatas.Add(_materialData.GetMaterialData(item)));
//                _playerStatusPanel.SetMaterialSprite(materialDatas);
//            })
//            .AddTo(_fieldManager);
//        }

//        //Playerが生成されたらPlayerの情報をPlayerのステータスを表示するクラスに渡す
//        if (_characterManager)
//        {
//            _characterManager.PlayerSpawn
//            .Subscribe(p =>
//            {
//                _playerStatusPanel.SetSlider(p);
//                _playerStatusPanel.SetMaterialViewPanel(p);
//            })
//            .AddTo(_characterManager);
//        }
//        ChangeTaskText(0);
//    }

//    /// <summary>
//    /// これからやるタスクを変える
//    /// </summary>
//    /// <param name="id"></param>
//    public void ChangeTaskText(int id)
//    {
//        Debug.Log($"ID:{id}");
//        _taskText.text = _taskManager.TaskList[id].TaskText;
//    }

//    public static Popup FindPopup(string path)
//    {
//        return s_popupList.Find(p => p.Path == path);
//    }
//}
