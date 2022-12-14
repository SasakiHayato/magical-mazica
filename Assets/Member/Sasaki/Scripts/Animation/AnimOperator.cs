using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.Data;

public enum AnimStateType
{
    Idle,
    Move,
    Attack,
}

/// <summary>
/// アニメーションの再生管理クラス
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimOperator : MonoBehaviour, IMonoDatableUni<AnimOperator>
{
    public struct AnimEvent
    {
        public Action Event { get; set; }
        public int Frame { get; set; }
    }

    Animator _anim = null;
    Coroutine _waitAnimCoroutine = null;
    List<Coroutine> _animEnvetCoroutineList = new List<Coroutine>();

    public bool EndCurrentAnim { get; private set; }
    public string CurrentAnim { get; private set; }

    AnimOperator IMonoDatableUni<AnimOperator>.GetData => this;
    string IMonoDatable.Path => nameof(AnimOperator);

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnPlay(string stateName, AnimEvent animEvent = default)
    {
        Initalize();

        _animEnvetCoroutineList.Add(StartCoroutine(OnExecuteEvent(animEvent)));
        OnExecute(stateName);
    }

    public void OnPlay(string stateName, List<AnimEvent> animEventList)
    {
        Initalize();

        foreach (AnimEvent animEvent in animEventList)
        {
            _animEnvetCoroutineList.Add(StartCoroutine(OnExecuteEvent(animEvent)));
        }

        OnExecute(stateName);
    }

    void Initalize()
    {

        EndCurrentAnim = false;

        if (_waitAnimCoroutine != null)
        {
            StopCoroutine(_waitAnimCoroutine);
        }

        if (_animEnvetCoroutineList.Count <= 0)
        {
            _animEnvetCoroutineList.ForEach(coroutine => StopCoroutine(coroutine));
            _animEnvetCoroutineList = new List<Coroutine>();
        }
    }

    void OnExecute(string stateName)
    {
        CurrentAnim = stateName;
        _anim.Play(stateName);
        _waitAnimCoroutine = StartCoroutine(OnWait());
    }

    IEnumerator OnWait()
    {
        yield return null;
        yield return new WaitForCurrentAnim(_anim);

        if (!_anim.GetCurrentAnimatorClipInfo(0)[0].clip.isLooping)
        {
            EndCurrentAnim = true;
        }

        _waitAnimCoroutine = null;
    }

    IEnumerator OnExecuteEvent(AnimEvent animEvent)
    {
        int frame = 0;

#if UNITY_EDITOR
        while (frame < animEvent.Frame * 20)
        {
            frame ++;
            yield return null;
        }
#else
        while (frame < animEvent.Frame)
        {
            frame ++;
            yield return null;
        }
#endif
        animEvent.Event?.Invoke();
    }
}
