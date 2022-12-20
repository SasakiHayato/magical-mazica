using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FieldData")]
public class FieldScriptableObject : ScriptableObject
{
    [SerializeField]
    int _mapVerSide;//縦の長さ

    [SerializeField]
    int _mapHorSide;//横の長さ

    [SerializeField]
    List<GameObject> _enemyObject;//敵のオブジェクト

    [SerializeField]
    GameObject _bossObj;

    [SerializeField]
    StageParts[] _spriteParts;

    public int MapVerSide { get => _mapVerSide; }
    public int MapHorSide { get => _mapHorSide; }
    public List<GameObject> EnemyObject { get => _enemyObject; }
    public GameObject BossObj { get => _bossObj; }

    /// <summary>
    /// WallTypeによってPartsを渡す
    /// </summary>
    /// <param name="key">WallType</param>
    /// <returns>Sprite</returns>
    public GameObject GetParts(WallType key)
    {
        for (int i = 0; i < _spriteParts.Length; i++)
        {
            if (_spriteParts[i].key == key)
            {
                return _spriteParts[i].value;
            }
        }
        return null;
    }

    [System.Serializable]
    class StageParts
    {
        public WallType key;
        public GameObject value;
    }
}

