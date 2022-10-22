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
    public void CreatePlayer(Transform spawnPosition)
    {
        _currentPlayer = Instantiate(_playerPrefab, spawnPosition.position, Quaternion.identity);
        _playerSpawn.OnNext(_currentPlayer);
    }
}
