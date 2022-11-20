using System.Collections.Generic;
using UnityEngine;

public interface IGameSetupable
{
    void GameSetup();
}

public interface IGameDisposable
{
    void GameDispose();
}

public interface IFieldObjectDatable
{
    GameObject Target { get; }
}

/// <summary>
/// �Q�[���\���̗����グ, �j����S���N���X
/// </summary>

public class GameController
{
    class FieldObjectData
    {
        List<IFieldObjectDatable> _dataList = new List<IFieldObjectDatable>();

        public void Add(IFieldObjectDatable field)
        {
            _dataList.Add(field);
        }

        public void Remove(IFieldObjectDatable field)
        {
            if (_dataList.Count <= 0) return;
            
            _dataList.Remove(field);
        }

        /// <summary>
        /// GameController����݂̂̌Ăяo����z��
        /// </summary>
        public void Dipose()
        {
            try
            {
                foreach (IFieldObjectDatable datable in _dataList)
                {
                    Remove(datable);
                    Object.Destroy(datable.Target);
                }
            }
            catch
            {
                Debug.Log("�A�N�Z�X����. GameController.FieldObjectData.Dispose()");
                _dataList = new List<IFieldObjectDatable>();
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

                GameControllerDisposer disposer = obj.AddComponent<GameControllerDisposer>();
                disposer.Action = DisposeInstance;
            }

            return s_instance;
        }
    }

    List<IGameSetupable> _setupList = new List<IGameSetupable>();
    List<IGameDisposable> _disposeList = new List<IGameDisposable>();

    FieldObjectData _fieldObjectData = new FieldObjectData();

    public UserInputManager UserInput { get; private set; } = new UserInputManager();

    public Transform Player { get; set; }

    public void AddGameSetupable(IGameSetupable setup)
    {
        _setupList.Add(setup);
    }

    public void AddGameDisposable(IGameDisposable dispose)
    {
        _disposeList.Add(dispose);
    }

    public void AddFieldObjectDatable(IFieldObjectDatable field)
    {
        _fieldObjectData.Add(field);
    }

    public void RemoveFieldObjectDatable(IFieldObjectDatable field)
    {
        _fieldObjectData.Remove(field);
    }

    /// <summary>
    /// �Q�[���\���̗����グ
    /// </summary>
    public void Setup()
    {
        _setupList.ForEach(s => s.GameSetup());
    }

    /// <summary>
    /// �Q�[���\���̔j��
    /// </summary>
    public void Dispose()
    {
        _disposeList.ForEach(d => d.GameDispose());
    }

    /// <summary>
    /// ���g��Instance�̔j��
    /// </summary>
    static void DisposeInstance()
    {
        Instance._fieldObjectData.Dipose();
        Instance.Dispose();

        Instance.Initalize();
    }

    void Initalize()
    {
        _setupList = new List<IGameSetupable>();
        _disposeList = new List<IGameDisposable>();
        _fieldObjectData = new FieldObjectData();
        UserInput = new UserInputManager();
    }
}
