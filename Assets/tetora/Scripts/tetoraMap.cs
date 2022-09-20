using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum MapState
{
    None, Wall, Floar
}
public class tetoraMap : MonoBehaviour
{
    [SerializeField]
    int _mapVerSide = 15;//�c�̒���
    [SerializeField]
    int _mapHorSide = 31;//���̒���
    [SerializeField]
    int _randomMaxNum = 4;
    Map[,] _map;

    /// <summary>�S�Ẵ}�X��ǂɂ���</summary>
    void SetWall()
    {
        for (int i = 0; i < _mapVerSide - 1; i++)
        {
            for (int j = 0; j < _mapHorSide - 1; j++)
            {
                _map[i, j].State = MapState.Wall;
            }
        }
    }
    /// <summary>�����_���ȃ|�W�V���������߂�</summary>
    void RandomPos()
    {
        int rndX = new System.Random().Next(_mapHorSide);
        int rndY = new System.Random().Next(_mapVerSide);
        int posX;
        int posY;
        if (rndX % 2 == 0)
        {
            posX = rndX;
        }
        else
        {
            if (rndX + 1 > _mapHorSide)
            {
                posX = rndX - 1;
            }
            else
            {
                posX = rndX + 1;
            }
        }

        if (rndY % 2 == 0)
        {
            posY = rndY;
        }
        else
        {
            if (rndY + 1 > _mapVerSide)
            {
                posY = rndY - 1;
            }
            else
            {
                posY = rndY + 1;
            }
        }
    }
    bool CheckDir()
    {
        return true;
    }
}
class Map
{
    MapState _state = MapState.None;
    public MapState State { get => _state; set => _state = value; }
}
