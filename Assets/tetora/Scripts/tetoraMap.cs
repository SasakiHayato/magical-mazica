using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void RandomPos()
    {

        int _rndX;
        int _rndY;
    }
}
class Map
{
    MapState _state = MapState.None;
    public MapState State { get => _state; set => _state = value; }
}
