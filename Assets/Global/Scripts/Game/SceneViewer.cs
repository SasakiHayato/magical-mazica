using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneViewer : MonoBehaviour
{
    public enum SceneType
    {
        Title = 0,
        Game = 1,
    }

    [SerializeField] string _bgmPath;
    [SerializeField] FadeManager _fadeManager;
    [SerializeField] FadeAnimationType _fadeAnimationType;

    static SceneViewer Instance = null;

    readonly float WaitTime = 0.5f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_fadeManager == null)
        {
            Load();
        }
        else
        {
            _fadeManager.Setup();
            OnWaitLoad().Forget();
        }
    }

    async UniTask OnWaitLoad()
    {
        Load();
        await _fadeManager.PlayAnimation(_fadeAnimationType, FadeType.Out);
        await UniTask.Delay(System.TimeSpan.FromSeconds(WaitTime));
    }

    void Load()
    {
        GameController.Instance.Setup();
        SoundManager.PlayRequest(SoundSystem.SoundType.BGM, _bgmPath);
    }

    async UniTask OnWaitUnLoad(SceneType sceneType)
    {
        await _fadeManager.PlayAnimation(_fadeAnimationType, FadeType.In);
        GameController.Instance.Dispose();
        await UniTask.Delay(System.TimeSpan.FromSeconds(WaitTime));

        SceneManager.LoadScene(sceneType.ToString());
    }

    public static void SceneLoad(SceneType sceneType)
    {
        Instance.OnWaitUnLoad(sceneType).Forget();
    }
}
