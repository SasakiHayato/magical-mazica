using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    /// <summary>Playerのプレハブ</summary>
    [SerializeField] Player _playerPrefab;
    /// <summary>Player生成位置</summary>
    [SerializeField] Transform _playerStartPos;
    private Player _currentPlayer;
    private Subject<Player> _playerSpawn = new Subject<Player>();
    /// <summary>Playerの生成を通知する</summary>
    public System.IObservable<Player> PlayerSpawn => _playerSpawn;

    public void Setup()
    {

    }

    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    public void CreatePlayer()
    {
        _currentPlayer = Instantiate(_playerPrefab, _playerStartPos);
        _playerSpawn.OnNext(_currentPlayer);
    }
}
