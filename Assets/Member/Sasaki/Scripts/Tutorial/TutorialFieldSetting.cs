using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFieldSetting : MapCreaterBase
{
    [SerializeField] Transform _playerPosition;

    public override Transform PlayerTransform { get => _playerPosition; protected set => _playerPosition = value; }

    protected override void Create()
    {
        
    }

    protected override void Initalize()
    {
        
    }
}
