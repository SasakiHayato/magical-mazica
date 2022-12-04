using System.Collections;
using UnityEngine;

public class CameraOperator : MonoBehaviour, IFieldEffectable
{
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] CameraData _cameraData;

    bool _onEvent = false;

    Vector3 Position => GameController.Instance.Player.position + _offsetPosition;

    void Awake()
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
}
