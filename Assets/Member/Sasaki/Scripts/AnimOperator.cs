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

    class ConnectData
    {
        public AnimStateType Current { get; set; }
        public AnimStateType Next { get; set; }
        public Func<bool> TransitionEvent { get; set; }

        public bool Transition() => TransitionEvent == null ? false : TransitionEvent.Invoke();
    }

    public struct AnimEvent
    {
        public Action Event { get; set; }
        public int AttributeFrame { get; set; }
    }

    [SerializeField] List<AnimStateData> _animDataList;

    AnimStateType _currentState;

    Animator _anim;
    List<ConnectData> _connectDataList = new List<ConnectData>();

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_animDataList.Count <= 0) return;

        foreach (ConnectData data in _connectDataList)
        {
            if (data.Current != _currentState && data.Transition())
            {
                PlayAnim(data.Next);
            }
        }
    }

    public void CreateConnectStateData(AnimStateType current, AnimStateType next, Func<bool> transitionEvent = null)
    {
        ConnectData data = new ConnectData
        {
            Current = current,
            Next = next,
            TransitionEvent = transitionEvent,
        };

        _connectDataList.Add(data);
    }

    public void PlayAnim(AnimStateType state, AnimEvent animEvent = default)
    {
        string name = _animDataList.FirstOrDefault(a => a.StateType == state).StateName;

        if (name != null)
        {
            _currentState = state;
            StartCoroutine(OnCallback(animEvent.Event, animEvent.AttributeFrame));
            _anim.Play(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    public void PlayAnim(AnimStateType state, List<AnimEvent> animEventList)
    {
        string name = _animDataList.FirstOrDefault(a => a.StateType == state).StateName;

        if (name != null)
        {
            _currentState = state;
            foreach (AnimEvent animEvent in animEventList)
            {
                StartCoroutine(OnCallback(animEvent.Event, animEvent.AttributeFrame));
            }
            
            _anim.Play(name);
        }
        else
        {
            Debug.LogWarning($"一致するデータがありません.{state}");
        }
    }

    IEnumerator OnCallback(Action action, int eventFrame)
    {
        int frame = 0;

        while (eventFrame > frame)
        {
            frame++;
            yield return null;
        }

        action?.Invoke();
    }
}
