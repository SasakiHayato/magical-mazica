using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
public class AnimOperator : MonoBehaviour
{
    [Serializable]
    class AnimStateData
    {
        [SerializeField] string _stateName;
        [SerializeField] AnimStateType _stateType;

        public string StateName => _stateName;
        public AnimStateType StateType => _stateType;
    }

    public struct AnimEvent
    {
        public Action Event { get; set; }
        public int AttributeFrame { get; set; }
    }

    public struct AnimTriggerEvent
    {
        public Action Event { get; set; }
        public Func<bool> Trigger { get; set; }
    }

    [SerializeField] List<AnimStateData> _animDataList;

    Animator _anim;
    Coroutine _waitAnimCoroutine = null;
    public bool IsEndCurrentAnim { get; private set; } = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="state">再生するステート</param>
    /// <param name="animEvent">イベントデータ</param>
    public void PlayAnim(AnimStateType state, AnimEvent animEvent = default)
    {
        string name = FindAnim(state);

        if (name != null)
        {
            StartCoroutine(OnCallbackEvent(animEvent.Event, animEvent.AttributeFrame));
            OnPlay(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="state">再生するステート</param>
    /// <param name="animEventList">イベントデータのリスト</param>
    public void PlayAnim(AnimStateType state, List<AnimEvent> animEventList)
    {
        string name = FindAnim(state);

        if (name != null)
        {
            foreach (AnimEvent animEvent in animEventList)
            {
                StartCoroutine(OnCallbackEvent(animEvent.Event, animEvent.AttributeFrame));
            }

            OnPlay(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="state">再生するステート</param>
    /// <param name="triggerEvent">イベントデータのデータ</param>
    public void PlayAnim(AnimStateType state, AnimTriggerEvent triggerEvent)
    {
        string name = FindAnim(state);

        if (name != null)
        {
            StartCoroutine(OnCallbackTriggerEvent(triggerEvent.Event, triggerEvent.Trigger));
            OnPlay(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    /// <summary>
    /// アニメーションの再生
    /// </summary>
    /// <param name="state">再生するステート</param>
    /// <param name="triggerEventList">イベントデータのデータリスト</param>
    public void PlayAnim(AnimStateType state, List<AnimTriggerEvent> triggerEventList)
    {
        string name = FindAnim(state);

        if (name != null)
        {
            foreach (AnimTriggerEvent animEvent in triggerEventList)
            {
                StartCoroutine(OnCallbackTriggerEvent(animEvent.Event, animEvent.Trigger));
            }

            OnPlay(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    void OnPlay(string stateName)
    {
        if (_waitAnimCoroutine != null)
        {
            StopCoroutine(_waitAnimCoroutine);
        }

        IsEndCurrentAnim = false;

        _anim.Play(stateName);
        _waitAnimCoroutine = StartCoroutine(OnWaitAnim());
    }

    IEnumerator OnCallbackEvent(Action action, int eventFrame)
    {
        int frame = 0;

        while (eventFrame > frame)
        {
            frame++;
            yield return null;
        }

        action?.Invoke();
    }

    IEnumerator OnCallbackTriggerEvent(Action action, Func<bool> func)
    {
        while (!func.Invoke())
        {
            yield return null;
        }

        action.Invoke();
    }

    IEnumerator OnWaitAnim()
    {
        yield return null;
        yield return new WaitForCurrentAnim(_anim);

        IsEndCurrentAnim = true;
    }

    string FindAnim(AnimStateType state) => _animDataList.FirstOrDefault(a => a.StateType == state).StateName;
}
