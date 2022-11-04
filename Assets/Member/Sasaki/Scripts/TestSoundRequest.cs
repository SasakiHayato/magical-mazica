using UnityEngine;

public class TestSoundRequest : MonoBehaviour
{
    [SerializeField] SoundManager _manager;

    public void OnRequest()
    {
        _manager.PlaySound(SoundSystem.SoundType.SEOther, "Test");
    }
}
