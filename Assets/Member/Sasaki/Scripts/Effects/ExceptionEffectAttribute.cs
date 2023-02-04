using UnityEngine;

public class ExceptionEffectAttribute : MonoBehaviour
{
    bool _onLoad = false;
    ParticleSystem _particle = null;

    [System.Obsolete]
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _particle.playOnAwake = false;
    }

    void Update()
    {
        if (!_onLoad) return;

        if (!_particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void OnLoad(Transform parent)
    {
        _particle.Play();

        transform.position = parent.position;

        _onLoad = true;
    }
}
