using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour, IFieldEffectable
{
    [SerializeField] CameraData _cameraData;
    [SerializeField] List<EventCamera> _eventCameraList;

    bool _onEvent = false;

    Vector3 Position => GameController.Instance.Player.position + _cameraData.View.Offset;

    static Vector3 s_position = Vector3.zero;
    static List<EventCamera> s_eventCameraList = null;

    void Awake()
    {
        s_eventCameraList = _eventCameraList;
    }

    void Start()
    {
        EffectStocker.Instance.AddFieldEffect(FieldEffect.EffectType.CmShake, this);
    }

    void Update()
    {
        if (GameController.Instance.Player == null || _onEvent) return;

        Move();
    }

    void Move()
    {
        s_position = Position;
        transform.position = Position;
    }

    void IFieldEffectable.Execute()
    {
        StartCoroutine(OnShake());
    }

    IEnumerator OnShake()
    {
        float timer = 0;

        _onEvent = true;
        while (timer < _cameraData.Shake.Time)
        {
            timer += Time.deltaTime;

            Vector3 magunitude = new Vector3(_cameraData.Shake.GetMagnitude, _cameraData.Shake.GetMagnitude, 0);

            transform.position = Position + (magunitude * _cameraData.Shake.Power);

            yield return null;
        }

        _onEvent = false;
    }

    public static void CallEvent(string path)
    {
        if (s_eventCameraList == null || s_eventCameraList.Count <= 0) return;

        EventCamera camera = s_eventCameraList.Find(e => e.Path == path);
        camera.SetTransition(s_position);
    }
}
