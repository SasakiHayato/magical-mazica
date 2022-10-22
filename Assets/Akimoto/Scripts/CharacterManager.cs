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
    /// <summary>Player�����ʒu</summary>
    [SerializeField] Transform _playerStartPos;
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
    public void CreatePlayer()
    {
        _currentPlayer = Instantiate(_playerPrefab, _playerStartPos);
        _playerSpawn.OnNext(_currentPlayer);
    }
}
