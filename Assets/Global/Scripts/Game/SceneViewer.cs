using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneViewer : MonoBehaviour
{
    enum SceneType
    {
        Title = 0,
        Game = 1,
    }

    [SerializeField] string _bgmPath;
    [SerializeField] FadeManager _fadeManager;

    void Start()
    {
        if (_fadeManager == null)
        {
            Load();
        }
    }

    void Load()
    {
        GameController.Instance.Setup();
        SoundManager.PlayRequest(SoundSystem.SoundType.BGM, _bgmPath);
    }
}
