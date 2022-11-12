using System.Collections.Generic;
using UnityEngine;

public interface IGameSetup
{
    void GameSetup();
}

public interface IGameDispose
{
    void GameDispose();
}

public class GameController
{
    static GameController s_instance = null;
    public static GameController Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new GameController();

                GameObject obj = new GameObject("GameControllerDisposer");
                obj.hideFlags = HideFlags.HideInHierarchy;

                GameControllerDisposer disposer = obj.AddComponent<GameControllerDisposer>();
                disposer.Action = DisposeInstance;
            }

            return s_instance;
        }
    }

    List<IGameSetup> _setupList = new List<IGameSetup>();
    List<IGameDispose> _disposeList = new List<IGameDispose>();

    public void AddGameSetup(IGameSetup setup)
    {
        _setupList.Add(setup);
    }

    public void AddGameDispose(IGameDispose dispose)
    {
        _disposeList.Add(dispose);
    }

    public void Setup()
    {
        _setupList.ForEach(s => s.GameSetup());
    }

    public void Dispose()
    {
        _disposeList.ForEach(d => d.GameDispose());
    }

    static void DisposeInstance()
    {
        Instance.Dispose();
        s_instance = null;
    }
}
