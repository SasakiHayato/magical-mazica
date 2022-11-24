using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CharacterManager : MonoBehaviour, IGameSetupable
{
    /// <summary>Playerのプレハブ</summary>
    [SerializeField] Player _playerPrefab;
    private Player _currentPlayer;
    private Subject<Player> _playerSpawn = new Subject<Player>();
    /// <summary>Playerの生成を通知する</summary>
    public System.IObservable<Player> PlayerSpawn => _playerSpawn;

    int IGameSetupable.Priority => 1;

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
    }

    public void Setup()
    {
        GetPlayer();
    }

    void IGameSetupable.GameSetup()
    {
        GetPlayer();
    }

    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    public void CreatePlayer(Transform spawnPosition)
    {
        if (GameController.Instance.Player == null)
        {
            _currentPlayer = Instantiate(_playerPrefab, spawnPosition.position, Quaternion.identity);
            _playerSpawn.OnNext(_currentPlayer);
        }
        else
        {
            GameController.Instance.Player.position = spawnPosition.position;
        }
    }
    public Player GetPlayer()
    {
        if (!_currentPlayer)
        {
            return _currentPlayer = FindObjectOfType<Player>();
        }
        return _currentPlayer;
    }
}
