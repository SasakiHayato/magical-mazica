using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneViewer : MonoBehaviour
{
    public enum SceneType
    {
        Title = 0,
        Game = 1,
        Boss = 2,
    }

    [SerializeField] string _bgmPath;
    [SerializeField] UserInputManager.InputType _defaultInputType;
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
        Setup();
    }

    void Setup()
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
        GameController.Instance.UserInput.SetInput(_defaultInputType);
    }

    async UniTask OnWaitUnLoad(SceneType sceneType)
    {
        if (_fadeManager != null)
        {
            await _fadeManager.PlayAnimation(_fadeAnimationType, FadeType.In);
        }
        
        GameController.Instance.Dispose();
        await UniTask.Delay(System.TimeSpan.FromSeconds(WaitTime));

        SceneManager.LoadScene((int)sceneType);
    }

    public static void SceneLoad(SceneType sceneType)
    {
        Instance.OnWaitUnLoad(sceneType).Forget();
    }

    public static void Initalize()
    {
        GameController.Instance.Dispose();
        Instance.Setup();
    }
}
