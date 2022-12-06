using UnityEngine;

public abstract class MapCreaterBase : MonoBehaviour, IGameSetupable, IGameDisposable
{
    int IGameSetupable.Priority => 1;
    public abstract Transform PlayerTransform { get; protected set; }

    void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
        GameController.Instance.AddGameDisposable(this);
    }

    void IGameSetupable.GameSetup()
    {

        Create();
    }

    void IGameDisposable.GameDispose()
    {
        Initalize();
    }

    protected abstract void Create();
    protected abstract void Initalize();
}
