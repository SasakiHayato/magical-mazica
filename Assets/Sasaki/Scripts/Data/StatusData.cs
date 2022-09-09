using System;
using UnityEngine;
using UniRx;

/// <summary>
/// Character�̃X�e�[�^�X�f�[�^
/// </summary>

[Serializable]
public class StatusData
{
    [SerializeField] int _hp;
    [SerializeField] float _speed;
    [SerializeField] int _power;

    int _maxHP;

    ReactiveProperty<int> _reactiveHP = new ReactiveProperty<int>();

    /// <summary>
    /// Event���������J
    /// </summary>
    public IObservable<int> ObservableHP => _reactiveHP; 

    /// <summary>
    /// ������
    /// </summary>
    public void Initalize()
    {
        _maxHP = _hp;
        _reactiveHP.Value = _hp;
    }

    public int CurrentHP => _reactiveHP.Value;
    public int MaxHP => _maxHP;
    public float Speed => _speed;
    public int Power => _power;
}
