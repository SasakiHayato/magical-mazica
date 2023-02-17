using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;
using SoundSystem;

public class ActionSetSE : BehaviourAction
{
    [SerializeField] SoundType _soundType;
    [SerializeField] string _soundPath;

    protected override bool Execute()
    {
        SoundManager.PlayRequest(_soundType, _soundPath);
        return true;
    }
}
