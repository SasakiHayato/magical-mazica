using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] Player _playerPrefab;
    /// <summary></summary>
    [SerializeField] Transform _playerStartPos;
    private Player _currentPlayer;
    private Subject<Player> _playerSpawn = new Subject<Player>();
    public System.IObservable<Player> PlayerSpawn => _playerSpawn;

    public void Setup()
    {

    }

    public void Create()
    {
        _currentPlayer = Instantiate(_playerPrefab, _playerStartPos);
        _playerSpawn.OnNext(_currentPlayer);
    }
}
