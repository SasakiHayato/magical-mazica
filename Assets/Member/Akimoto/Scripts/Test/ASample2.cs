using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class ASample2 : MonoBehaviour
{
    //[SerializeField] GUIManager _guiManager;
    //[SerializeField] CharacterManager _characterManager;
    private Subject<List<RawMaterialID>> _subject = new Subject<List<RawMaterialID>>();
    public System.IObservable<List<RawMaterialID>> Subject => _subject;

    private void Start()
    {
        //’Ê’m
        _subject.OnNext(new List<RawMaterialID> { RawMaterialID.BombBean, RawMaterialID.PowerPlant });

        //_guiManager.Setup();
        //_characterManager.CreatePlayer();
    }
}
