using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MapState
{
    Wall, Floar
}
[System.Serializable]
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    FieldScriptableObject _scriptableObject;
    [SerializeField]
    GameObject[] _mapChip;//0:��,1:���̓�,2:
    [SerializeField]
    GameObject _parentObj;
    [SerializeField]
    int _mapScale = 14;
    [SerializeField]
    float _tileSize = 3;
    Map[,] _map;
    int _startDigPos;//�@��n�߂�n�_
    bool _isCreate = default;//����
    private void Start()
    {
        SetWall();
        StartDig();
        CreateEnemy();
    }
    /// <summary>�S�Ẵ}�X��ǂɂ���</summary>
    void SetWall()
    {
        _map = new Map[_scriptableObject.MapHorSide, _scriptableObject.MapVerSide];
        for (int y = 0; y < _scriptableObject.MapVerSide; y++)
        {
            for (int x = 0; x < _scriptableObject.MapHorSide; x++)
            {
                _map[x, y] = new Map(x + y * _scriptableObject.MapHorSide);
                _map[x, y].State = MapState.Wall;
            }
        }
    }
    /// <summary>�����_���ȊJ�n�n�_�����߂�</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_scriptableObject.MapHorSide - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_scriptableObject.MapVerSide - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _scriptableObject.MapHorSide;
    }
    /// <summary>4�����������_���ȏ��ԂŕԂ�</summary>
    /// <param name="id">�J�n�n�_��id</param>
    /// <returns>4������id</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_scriptableObject.MapHorSide * 2),id - _scriptableObject.MapHorSide), //��
            (id + _scriptableObject.MapHorSide * 2,id + _scriptableObject.MapHorSide), //��
            (id + 2,id + 1), //�E
            (id - 2,id - 1) //��
        };
        for (int i = 0; i < twoTargetDirs.Length; i++)
        {
            //���Ԃ��V���b�t������
            int r = UnityEngine.Random.Range(0, twoTargetDirs.Length);
            var tmp = twoTargetDirs[i];
            twoTargetDirs[i] = twoTargetDirs[r];
            twoTargetDirs[r] = tmp;
        }
        foreach ((int two, int one) dir in twoTargetDirs)
        {
            //two:2���id ,one:1���id
            if (dir.two < 0 || dir.two > _map.Length - 1)//�͈͓���id�̂ݒl��Ԃ�
            {
                continue;
            }
            //���̃T�C�Y�𒴂��Ȃ���
            if (id % _scriptableObject.MapHorSide < 1 || id % _scriptableObject.MapHorSide >= _scriptableObject.MapHorSide - 1)
            {
                continue;
            }
            //2�悪�ǂ��m�F
            if (_map[dir.two % _scriptableObject.MapHorSide, dir.two / _scriptableObject.MapHorSide].State != MapState.Wall)
            {
                continue;
            }
            //1�悪�ǂ��m�F
            if (_map[dir.one % _scriptableObject.MapHorSide, dir.one / _scriptableObject.MapHorSide].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _scriptableObject.MapHorSide == 0 || dir.two / _scriptableObject.MapHorSide == _scriptableObject.MapVerSide - 1)
            {
                continue;
            }
            if (dir.two % _scriptableObject.MapHorSide == 0 || dir.two % _scriptableObject.MapHorSide == _scriptableObject.MapHorSide - 1)
            {
                continue;
            }
            yield return dir;
        }
    }
    /// <summary>�����@�鏈��</summary>
    /// <param name="id">�J�n�n�_</param>
    void Dig(int id)
    {
        _map[id % _scriptableObject.MapHorSide, id / _scriptableObject.MapHorSide].State = MapState.Floar;
        foreach (var posId in CheckDir(id))
        {
            _map[posId.oneTarget % _scriptableObject.MapHorSide, posId.oneTarget / _scriptableObject.MapHorSide].State = MapState.Floar;
            Dig(posId.twoTarget);
        }
    }
    /// <summary>�@��n�߂�</summary>
    void StartDig()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _map)
        {
            if (pos.State == MapState.Floar)
            {
                continue;
            }
            var wall = Instantiate(_mapChip[0]);//
            wall.transform.SetParent(_parentObj.transform);
            wall.transform.position = new Vector2(pos.Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                pos.Id / _scriptableObject.MapHorSide  - _scriptableObject.MapVerSide / 2 ) * _tileSize;
        }
    }
    void CreateEnemy()
    {
        List<Map> generatablePosList = new List<Map>();
        foreach (var floar in _map)
        {
            if (floar.State == MapState.Floar)//Floar�̏ꏊ��ۑ�
            {
                generatablePosList.Add(floar);
            }
        }
        
        for (int i = 0; i < _scriptableObject.EnemyCount; i++)
        {
            //Enemy�̏o��
            
            int enemyRnd = new System.Random().Next(0, _scriptableObject.EnemyObject.Length);
            GameObject obj = Instantiate(_scriptableObject.EnemyObject[enemyRnd]);
            int random = new System.Random().Next(0, generatablePosList.Count);//�ŏ��ɗN������ꏊ
            obj.transform.position = new Vector2(generatablePosList[random].Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                generatablePosList[random].Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _tileSize;
        }
    }
}
class Map
{
    MapState _state = MapState.Wall;
    public MapState State { get => _state; set => _state = value; }
    public readonly int Id;
    public Map(int id)
    {
        Id = id;
    }
}
