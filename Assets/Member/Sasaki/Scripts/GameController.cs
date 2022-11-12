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

public interface IFieldObject
{
    GameObject Target { get; }
}

public class GameController
{
    class FieldObjectData
    {
        List<IFieldObject> _objectList = new List<IFieldObject>();

        public void Add(IFieldObject field)
        {
            _objectList.Add(field);
        }

        public void Remove(IFieldObject field)
        {
            _objectList.Remove(field);
        }

        /// <summary>
        /// GameControllerÇ©ÇÁÇÃÇ›ÇÃåƒÇ—èoÇµÇëzíË
        /// </summary>
        public void Dipose()
        {
            foreach (IFieldObject fieldObject in _objectList)
            {
                _objectList.Remove(fieldObject);
                Object.Destroy(fieldObject.Target);
            }
        }
    }

    // Note. Singleton
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

    FieldObjectData _fieldObjectData = new FieldObjectData();

    public void AddGameSetup(IGameSetup setup)
    {
        _setupList.Add(setup);
    }

    public void AddGameDispose(IGameDispose dispose)
    {
        _disposeList.Add(dispose);
    }

    public void AddFieldObject(IFieldObject field)
    {
        _fieldObjectData.Add(field);
    }

    public void RemoveFieldObject(IFieldObject field)
    {
        _fieldObjectData.Remove(field);
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
        Instance._fieldObjectData.Dipose();
        Instance.Dispose();

        s_instance = null;
    }
}
