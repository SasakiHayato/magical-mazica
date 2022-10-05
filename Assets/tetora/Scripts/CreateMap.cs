using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MapState
{
    Wall, Floar
}
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    int _mapVerSide = 15;//�c�̒���
    [SerializeField]
    int _mapHorSide = 31;//���̒���
    [SerializeField]
    GameObject _wallObj;
    Map[,] _map;
    int _startDigPos;//�@��n�߂�n�_
    private void Start()
    {
        SetWall();
        StartDig();
    }
    /// <summary>�S�Ẵ}�X��ǂɂ���</summary>
    void SetWall()
    {
        _map = new Map[_mapHorSide, _mapVerSide];
        for (int y = 0; y < _mapVerSide; y++)
        {
            for (int x = 0; x < _mapHorSide; x++)
            {
                _map[x, y] = new Map(x + y * _mapHorSide);
                _map[x, y].State = MapState.Wall;
            }
        }
    }
    /// <summary>�����_���ȊJ�n�n�_�����߂�</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_mapHorSide - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_mapVerSide - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _mapHorSide;
    }
    /// <summary>4�����������_���ȏ��ԂŕԂ�</summary>
    /// <param name="id">�J�n�n�_��id</param>
    /// <returns>4������id</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_mapHorSide * 2),id - _mapHorSide), //��
            (id + _mapHorSide * 2,id + _mapHorSide), //��
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
            if (id % _mapHorSide < 1 || id % _mapHorSide >= _mapHorSide - 1)
            {
                continue;
            }
            //2�悪�ǂ��m�F
            if (_map[dir.two % _mapHorSide, dir.two / _mapHorSide].State != MapState.Wall)
            {
                continue;
            }
            //1�悪�ǂ��m�F
            if (_map[dir.one % _mapHorSide, dir.one / _mapHorSide].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _mapHorSide == 0 || dir.two / _mapHorSide == _mapVerSide - 1)
            {
                continue;
            }
            if (dir.two % _mapHorSide == 0 || dir.two % _mapHorSide == _mapHorSide - 1)
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
        _map[id % _mapHorSide, id / _mapHorSide].State = MapState.Floar;
        foreach (var posId in CheckDir(id))
        {
            _map[posId.oneTarget % _mapHorSide, posId.oneTarget / _mapHorSide].State = MapState.Floar;
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
            var wall = Instantiate(_wallObj);
            wall.transform.position = new Vector2(pos.Id % _mapHorSide, pos.Id / _mapHorSide);
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
