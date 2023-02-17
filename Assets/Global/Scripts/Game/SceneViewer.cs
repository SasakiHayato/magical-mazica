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
        Tutorial = 3,
        Result = 4,
    }

    [SerializeField] string _bgmPath;
    [SerializeField] InputUserType _defaultInputType;
    [SerializeField] SceneType _sceneType;
    [SerializeField] FadeManager _fadeManager;
    [SerializeField] FadeAnimationType _fadeAnimationType;

    static SceneViewer Instance = null;

    readonly float WaitTime = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_sceneType == SceneType.Title)
        {
            GameController.Instance.AddGameSetupable(new UIInputSetScene());
            GameController.Instance.AddGameSetupable(new UIInputOption());
            GameController.Instance.AddGameSetupable(new UIInputOptionSound());
            
            InputSetting.UIInputOperate.OperateRequest(new UIInputSelectTitle());
        }

        if (_sceneType == SceneType.Result)
        {
            GameController.Instance.AddGameSetupable(new UIInputBackTitle());
        }

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
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
        GameController.Instance.Setup(2);
    }

    void Load()
    {
        GameController.Instance.Setup(1);
        //SoundManager.PlayRequest(SoundSystem.SoundType.BGM, _bgmPath);
        SoundManager.PlayBGM(_bgmPath);
        InputSetting.ChangeInputUser(_defaultInputType);
    }

    async UniTask OnWaitUnLoad(SceneType sceneType, bool isWaitSound)
    {
        if (_fadeManager != null)
        {
            await _fadeManager.PlayAnimation(_fadeAnimationType, FadeType.In);
        }

        if (isWaitSound)
        {
            await UniTask.WaitUntil(() => SoundManager.IsStopAllSound);
        }
        
        GameController.Instance.Dispose();
        await UniTask.Delay(System.TimeSpan.FromSeconds(WaitTime));
        GameController.DisposeLocalData();

        SceneManager.LoadScene((int)sceneType);
    }

    public static void SceneLoad(SceneType sceneType, bool isWaitSound = false)
    {
        SoundManager.StopBGM();
        Instance.OnWaitUnLoad(sceneType, isWaitSound).Forget();
    }

    public static void Initalize()
    {
        GameController.Instance.Dispose();
        Instance.Setup();
    }
}
