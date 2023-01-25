using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectType
{
    Player,
    Enemy,
    Obstacle,
}

public interface IGameSetupable
{
    int Priority { get; }
    void GameSetup();
}

public interface IGameLateSetupable
{
    int Priority { get; }
    void GameLateSetup();
}

public interface IGameDisposable
{
    void GameDispose();
}

public interface IFieldObjectDatable
{
    ObjectType ObjectType { get; }
    GameObject Target { get; }
}

public interface IDamagable
{
    ObjectType ObjectType { get; }
    void AddDamage(int damage);
}

public interface IDamageForceble
{
    void OnFoece(Vector2 direction);
}

/// <summary>
/// ゲーム構造の立ち上げ, 破棄を担うクラス
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

        public IEnumerable<IFieldObjectDatable> GetObjectData(ObjectType objectType)
        {
            return _dataList.Where(d => d.ObjectType == objectType);
        }

        /// <summary>
        /// GameControllerからのみの呼び出しを想定
        /// </summary>
        public void Dispose()
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
                Debug.Log("アクセス拒否. GameController.FieldObjectData.Dispose()");
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
            }

            return s_instance;
        }
    }

    List<IGameSetupable> _setupList = new List<IGameSetupable>();
    List<IGameLateSetupable> _lateSetuplist = new List<IGameLateSetupable>();
    List<IGameDisposable> _disposeList = new List<IGameDisposable>();

    FieldObjectData _fieldObjectData = new FieldObjectData();
    Transform _player = null;

    int _currentMapHierarchy = 1;

    public Transform Player => _player;
    public Transform SetPlayer 
    {
        set 
        {
            if (value.GetComponent<Player>() != null)
            {
                _player = value;
            }
        }
    }

    public bool OnInputEvent { get; set; }

    public readonly int MaxMapHierarchy = 2;

    public void AddGameSetupable(IGameSetupable setup)
    {
        _setupList.Add(setup);
    }

    public void AddGameLateSetupble(IGameLateSetupable lateSetup)
    {
        _lateSetuplist.Add(lateSetup);
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

    public IEnumerable<IFieldObjectDatable> GetFieldObjectDatable(ObjectType objectType)
    {
        return _fieldObjectData.GetObjectData(objectType);
    }

    /// <summary>
    /// ゲーム構造の立ち上げ
    /// </summary>
    public void Setup(int id)
    {
        switch (id)
        {
            case 1:
                _setupList.OrderBy(s => s.Priority).ToList().ForEach(s => s.GameSetup());
                break;
            case 2:
                if (_lateSetuplist.Count > 0)
                {
                    _lateSetuplist.OrderBy(s => s.Priority).ToList().ForEach(s => s.GameLateSetup());
                }
                break;
        }
    }

    /// <summary>
    /// ゲーム構造の破棄
    /// </summary>
    public void Dispose()
    {
        _fieldObjectData.Dispose();
        _disposeList.ForEach(d => d.GameDispose());
    }

    public void SetNextMap(bool isTutorial = false)
    {
        if (isTutorial)
        {
            SceneViewer.SceneLoad(SceneViewer.SceneType.Game);
            return;
        }

        if (_currentMapHierarchy >= MaxMapHierarchy)
        {
            SceneViewer.SceneLoad(SceneViewer.SceneType.Boss);
        }
        else
        {
            _currentMapHierarchy++;
            SceneViewer.Initalize();
        }
    }

    /// <summary>
    /// 自身のInstanceの破棄
    /// </summary>
    public static void DisposeLocalData()
    {
        Instance.Initalize();
    }

    void Initalize()
    {
        _setupList = new List<IGameSetupable>();
        _lateSetuplist = new List<IGameLateSetupable>();
        _disposeList = new List<IGameDisposable>();
        _fieldObjectData = new FieldObjectData();

        _player = null;
    }
}
