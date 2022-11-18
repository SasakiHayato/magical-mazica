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
/// �A�j���[�V�����̍Đ��Ǘ��N���X
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
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="state">�Đ�����X�e�[�g</param>
    /// <param name="animEvent">�C�x���g�f�[�^</param>
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
            Debug.LogWarning($"��v����f�[�^������܂���.{state}");
        }
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="state">�Đ�����X�e�[�g</param>
    /// <param name="animEventList">�C�x���g�f�[�^�̃��X�g</param>
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
            Debug.LogWarning($"��v����f�[�^������܂���.{state}");
        }
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="state">�Đ�����X�e�[�g</param>
    /// <param name="triggerEvent">�C�x���g�f�[�^�̃f�[�^</param>
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
            Debug.LogWarning($"��v����f�[�^������܂���.{state}");
        }
    }

    /// <summary>
    /// �A�j���[�V�����̍Đ�
    /// </summary>
    /// <param name="state">�Đ�����X�e�[�g</param>
    /// <param name="triggerEventList">�C�x���g�f�[�^�̃f�[�^���X�g</param>
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
            Debug.LogWarning($"��v����f�[�^������܂���.{state}");
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
