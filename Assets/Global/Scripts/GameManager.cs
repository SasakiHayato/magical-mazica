using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] FieldManager _fieldManager;
    [SerializeField] GUIManager _guiManager;
    CharacterManager _characterManager;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //_guiManager.Setup();
        //_fieldManager.Setup();
        _characterManager = FindObjectOfType<CharacterManager>();
    }

    public Player GetPlayer()
    {
        return _characterManager.GetPlayer();
    } 
}
