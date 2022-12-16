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
            s_instance = s_instance == null ? new GameController() : s_instance;
            
            return s_instance;
        }
    }

    List<IGameSetupable> _setupList = new List<IGameSetupable>();
    List<IGameDisposable> _disposeList = new List<IGameDisposable>();

    FieldObjectData _fieldObjectData = new FieldObjectData();

    int _currentMapHierarchy = 1;

    public Transform Player { get; set; }

    public readonly int MaxMapHierarchy = 2;

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

    public IEnumerable<IFieldObjectDatable> GetFieldObjectDatable(ObjectType objectType)
    {
        return _fieldObjectData.GetObjectData(objectType);
    }

    /// <summary>
    /// ゲーム構造の立ち上げ
    /// </summary>
    public void Setup()
    {
        _setupList.OrderBy(s => s.Priority).ToList().ForEach(s => s.GameSetup());
    }

    /// <summary>
    /// ゲーム構造の破棄
    /// </summary>
    public void Dispose()
    {
        _fieldObjectData.Dispose();
        _disposeList.ForEach(d => d.GameDispose());
    }

    public void SetNextMap()
    {
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
        _disposeList = new List<IGameDisposable>();
        _fieldObjectData = new FieldObjectData();

        Player = null;
    }
}
