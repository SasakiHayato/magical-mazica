using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport
}
[System.Serializable]
public class CreateMap : MonoBehaviour
{
    [SerializeField]
    FieldScriptableObject _scriptableObject;
    [SerializeField]
    GameObject _wallObj;
    [SerializeField]
    GameObject _parentObj;
    [SerializeField]
    int _mapScale = 14;
    [SerializeField]
    int _releDis = 3;//���}�X������

    List<Map> _floarList = new List<Map>();
    float _wallObjSize = 3;//�}�b�v���̃T�C�Y
    Map[,] _map;
    int _startDigPos;//�@��n�߂�n�_
    public Transform PlayerTransform { get; private set; }
    /// <summary>�����ݒ�</summary>
    public void InitialSet()
    {
        //�ǃI�u�W�F�N�g��ScaleSize������
        _wallObjSize = _wallObj.transform.localScale.x;
        SetWall();
        StartDig();
        DecisionPlayerPos();
        InstantiateEnemy();
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
                _map[x, y].IsGenerate = false;
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
            int r = new System.Random().Next(0, twoTargetDirs.Length);
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
        _map[id % _scriptableObject.MapHorSide, id / _scriptableObject.MapHorSide].IsGenerate = true;
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
                var emptyObj = new GameObject();//��̃I�u�W�F�N�g�𐶐�
                emptyObj.name = "Floar";
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                var wall = Instantiate(_wallObj);
                SetTransform(wall, pos);
            }
        }
    }
    /// <summary>�I�u�W�F�N�g����ׂ�</summary>
    /// <param name="obj">���ׂ����I�u�W�F�N�g</param>
    /// <param name="map">���ׂ���Map</param>
    void SetTransform(GameObject obj, Map map)
    {
        obj.transform.SetParent(_parentObj.transform);
        obj.transform.position = new Vector2(map.Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
            map.Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
        obj.name = $"ID:{map.Id}";
        map.ObjTransform = obj.transform;
    }
    /// <summary>
    /// �G����ނ̐���������
    /// </summary>
    void InstantiateEnemy()
    {
        for (int i = 0; i < _scriptableObject.EnemyObject.Count; i++)
        {
            GameObject enemy = Instantiate(_scriptableObject.EnemyObject[i]);
            SetEnemyPos(enemy);
        }
    }
    /// <summary>
    /// �G�̐�������ꏊ�����߂�
    /// </summary>
    /// <param name="enemy">�G��GameObject</param>
    void SetEnemyPos(GameObject enemy)
    {
        int random = new System.Random().Next(0, GetFloar().Count);//���̐����烉���_���ɐ������擾

        if (GetFloar()[random] == null)
        {
            SetEnemyPos(enemy);
        }
        if (GetFloar()[random].IsGenerate != true)
        {
            SetEnemyPos(enemy);
        }
        else
        {
            enemy.transform.position = new Vector2(GetFloar()[random].Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
            GetFloar()[random].Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
            enemy.name = $"EnemyID:{GetFloar()[random].Id}";
            GetFloar()[random].IsGenerate = false;
        }
    }
    /// <summary>���I�u�W�F�N�g����������</summary>
    /// <returns>���I�u�W�F�N�g��List</returns>
    List<Map> GetFloar()
    {
        foreach (var floar in _map)
        {
            if (floar.State == MapState.Floar)//Floar�̏ꏊ��ۑ�
            {
                if (_floarList.Contains(floar))
                {
                    continue;
                }
                else
                {
                    _floarList.Add(floar);
                }
            }
        }
        return _floarList;
    }
    /// <summary>Player�����̏ꏊ�����߂�</summary>
    public void DecisionPlayerPos()
    {
        int rndId = new System.Random().Next(0, GetFloar().Count);//���I�u�W�F�N�g�̓����Ă���List�̗v�f�����烉���_���Ȓl���擾
                                                                  // Map rndMap = GetFloar()[rndId];//���I�u�W�F�N�g�̃����_���ȃI�u�W�F�N�g���擾
        foreach (var item in _map)
        {
            if (item.Id + _scriptableObject.MapHorSide == GetFloar()[rndId].Id)
            {
                if (item.State == MapState.Wall)
                {
                    GetFloar()[rndId].State = MapState.Player;//Player��������ꏊ
                    for (int i = 0; i < _releDis; i++)
                    {
                        foreach (var point in CheckDir(GetFloar()[rndId], i))
                        {
                            _map[point % _scriptableObject.MapHorSide, point / _scriptableObject.MapHorSide].IsGenerate = false;
                        }
                    }
                    Debug.Log($"PlayerID{GetFloar()[rndId].Id}");
                    PlayerTransform = GetFloar()[rndId].ObjTransform;
                    return;
                }
            }
        }
        DecisionPlayerPos();
    }

    /// <summary>
    /// ���͂̃}�X�𒲂ׂ�
    /// </summary>
    /// <param name="mapInfo">���ׂ������S�̃}�X</param>
    /// <param name="releDis">���}�X���ׂ邩</param>
    /// <returns>����̃}�X</returns>
    IEnumerable<int> CheckDir(Map mapInfo, int releDis)
    {
        //�E���𒲂ׂ�
        if (mapInfo.Id % _scriptableObject.MapHorSide + releDis < _scriptableObject.MapHorSide)
        {
            yield return _map[mapInfo.Id % _scriptableObject.MapHorSide + releDis, mapInfo.Id / _scriptableObject.MapHorSide].Id;
        }

        //�����𒲂ׂ�
        if (mapInfo.Id % _scriptableObject.MapHorSide - releDis >= 0)
        {
            yield return _map[mapInfo.Id % _scriptableObject.MapHorSide - releDis, mapInfo.Id / _scriptableObject.MapHorSide].Id;
        }

        //�����𒲂ׂ�
        if (mapInfo.Id / _scriptableObject.MapHorSide - releDis >= 0)
        {
            yield return _map[mapInfo.Id % _scriptableObject.MapHorSide, mapInfo.Id / _scriptableObject.MapHorSide - releDis].Id;
        }

        //�㑤�𒲂ׂ�
        if (mapInfo.Id / _scriptableObject.MapHorSide + releDis < _scriptableObject.MapVerSide)
        {
            yield return _map[mapInfo.Id % _scriptableObject.MapHorSide, mapInfo.Id / _scriptableObject.MapHorSide + releDis].Id;
        }
    }

    //void SetTeleportPos(GameObject[] teleporterObj)
    //{
    //    if (TESTTELEPORT != null)
    //    {
    //        return;
    //    }
    //    List<Map> leftMapsList = new List<Map>();
    //    List<Map> rightMapsList = new List<Map>();
    //    int leftLine = _scriptableObject.MapHorSide / 3; //3�����������̍��̐�
    //    int rightLine = _scriptableObject.MapHorSide / 3 * 2;//3���������̉E�̐�
    //    foreach (var item in GetFloar())
    //    {
    //        if (item.Id % _scriptableObject.MapHorSide <= leftLine)
    //        {
    //            leftMapsList.Add(item);
    //        }
    //        if (item.Id % _scriptableObject.MapHorSide >= rightLine)
    //        {
    //            rightMapsList.Add(item);
    //        }
    //    }
    //    int leftRnd = new System.Random().Next(0, leftMapsList.Count);
    //    int rightRnd = new System.Random().Next(0, rightMapsList.Count);
    //    var leftTele = Instantiate(TESTTELEPORT[0]);
    //    var rightTele = Instantiate(TESTTELEPORT[1]);
    //    leftTele.transform.position = leftMapsList[leftRnd].ObjTransform.position;
    //    rightTele.transform.position = rightMapsList[rightRnd].ObjTransform.position;
    //}
    class Map
    {
        MapState _state = MapState.Wall;
        public MapState State { get => _state; set => _state = value; }
        Transform _objTransform;
        public Transform ObjTransform { get => _objTransform; set => _objTransform = value; }
        bool _isGenerate;
        public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }
        public readonly int Id;
        public Map(int id)
        {
            Id = id;
        }
    }
}

