using UnityEngine;

public class TestEffectUser : MonoBehaviour
{
    EffectRequester _effectRequester = new EffectRequester();

    void Start()
    {
        _effectRequester
            .LoadAsset()
            .SetupEffect("Test", transform);
    }

    public void Use()
    {
        _effectRequester.Use("Test", transform.position);
    }
}
