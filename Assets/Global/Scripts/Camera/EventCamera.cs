using System.Collections;
using UnityEngine;

public interface ICamearaEventable
{
    void Setup(Camera camera);
    void OnEvent();
    bool OnExecute();
    void DisposeEvent();
}

public class EventCamera : MonoBehaviour
{
    public enum TransitionType
    {
        Awake,
        Lerp,
    }

    [SerializeField] string _path;
    [SerializeField] TransitionType _transitionType;
    [SerializeReference, SubclassSelector] ICamearaEventable _eventable;

    bool _onEvent = false;
    Vector3 _savePosition = Vector3.zero;

    Camera _camera;

    public string Path => _path;
    
    private void Start()
    {
        if (!TryGetComponent(out _camera))
        {
            _camera = gameObject.AddComponent<Camera>();
        }
        
        _eventable?.Setup(_camera);
        _camera.enabled = false;

        _savePosition = transform.position;
    }

    private void Update()
    {
        if (!_onEvent) return;

        if (_eventable == null)
        {
            DisposeEvent();
            return;
        }

        if (_eventable.OnExecute())
        {
            DisposeEvent();
        }
    }

    public void SetTransition(Vector3 matserPosition)
    {
        switch (_transitionType)
        {
            case TransitionType.Awake:
                OnEvent();
                break;
            case TransitionType.Lerp:
                StartCoroutine(OnTransition(matserPosition));
                break;
        }
        
    }

    IEnumerator OnTransition(Vector3 masterposition)
    {
        float time = 0;
        bool frag = true;

        while (frag)
        {
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(masterposition, _savePosition, time);

            if (transform.position == _savePosition)
            {
                frag = false;
            }

            yield return null;
        }

        OnEvent();
    }

    void OnEvent()
    {
        _onEvent = true;
        _camera.enabled = true;
        _eventable?.OnEvent();
    }

    void DisposeEvent()
    {
        _onEvent = false;
        _eventable?.DisposeEvent();
        _camera.enabled = false;
    }
}
