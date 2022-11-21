using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class GameManager : MonoBehaviour, IGameSetupable
{
    [SerializeField] bool _isDebug;
    [SerializeField] FieldManager _fieldManager;
    [SerializeField] GUIManager _guiManager;
    CharacterManager _characterManager;
    public static GameManager Instance { get; private set; }

    int IGameSetupable.Priority => 1;

    private void Awake()
    {
        Instance = this;
        GameController.Instance.AddGameSetupable(this);
    }

    private void Start()
    {
        if (!_isDebug)
        {
            //_guiManager.Setup();
            //_fieldManager.Setup();
        }
        
    }

    void IGameSetupable.GameSetup()
    {
        _characterManager = FindObjectOfType<CharacterManager>();
    }

    public Player GetPlayer()
    {
        return _characterManager.GetPlayer();
    }
}
