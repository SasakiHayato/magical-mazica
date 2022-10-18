using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] Player _playerPrefab;
    [SerializeField] Transform _startPos;
    public Player CurrentPlayer { get; private set; }

    public void Setup()
    {

    }

    public void Create()
    {
        CurrentPlayer = Instantiate(_playerPrefab, _startPos);

    }
}
