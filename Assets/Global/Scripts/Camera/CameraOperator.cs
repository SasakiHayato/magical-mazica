using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour, IFieldEffectable<float>
{
    [SerializeField] MiniMapCameraData _miniMapCameraData;
    [SerializeField] CameraData _cameraData;
    [SerializeField] List<EventCamera> _eventCameraList;

    Camera _minimapCm = null;
    bool _onEvent = false;

    Vector3 Position
    {
        get
        {
            try
            {
                return GameController.Instance.Player.position;
            }
            catch
            {
                return Vector3.zero;
            }
        }
    }

    FieldEffect.EffectType IFieldEffectDatable.EffectType => FieldEffect.EffectType.CmShake;

    static Vector3 s_position = Vector3.zero;
    static List<EventCamera> s_eventCameraList = null;

    public static float ShakePower { get; private set; }

    void Awake()
    {
        s_eventCameraList = _eventCameraList;
    }

    void Start()
    {
        ShakePower = _cameraData.Shake.Power;
        EffectStocker.Instance.AddFieldEffect(this);
        CreateMiniMapCamera();
    }

    void Update()
    {
        if (GameController.Instance.Player == null || _onEvent) return;

        Move();
    }

    void CreateMiniMapCamera()
    {
        if (_miniMapCameraData.RenderTexture == null) return;

        _minimapCm = new GameObject("MiniMapCm").AddComponent<Camera>();
        _minimapCm.targetTexture = _miniMapCameraData.RenderTexture;
        _minimapCm.cullingMask = _miniMapCameraData.ViewLayer;
    }

    void Move()
    {
        s_position = Position + _cameraData.View.Offset;

        transform.position = Position + _cameraData.View.Offset;

        if (_minimapCm != null)
        {
            _minimapCm.transform.position = Position + _miniMapCameraData.OffsetPosition;
        }
    }

    void IFieldEffectable<float>.Execute(float value)
    {
        StartCoroutine(OnShake(value));
    }

    IEnumerator OnShake(float power)
    {
        float timer = 0;

        _onEvent = true;
        while (timer < _cameraData.Shake.Time)
        {
            timer += Time.deltaTime;

            Vector3 magunitude = new Vector3(_cameraData.Shake.GetMagnitude, _cameraData.Shake.GetMagnitude, 0);

            transform.position = Position + _cameraData.View.Offset + (magunitude * power);

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
