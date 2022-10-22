using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    /// <summary>Player�̃v���n�u</summary>
    [SerializeField] Player _playerPrefab;
    private Player _currentPlayer;
    private Subject<Player> _playerSpawn = new Subject<Player>();
    /// <summary>Player�̐�����ʒm����</summary>
    public System.IObservable<Player> PlayerSpawn => _playerSpawn;

    public void Setup()
    {

    }

    /// <summary>
    /// �v���C���[�𐶐�
    /// </summary>
    public void CreatePlayer(Transform spawnPosition)
    {
        _currentPlayer = Instantiate(_playerPrefab, spawnPosition.position, Quaternion.identity);
        _playerSpawn.OnNext(_currentPlayer);
    }
}
