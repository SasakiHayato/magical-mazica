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
    GameObject[] TESTTELEPORT;

    float _wallObjSize = 3;//�}�b�v���̃T�C�Y
    Map[,] _map;
    int _startDigPos;//�@��n�߂�n�_
    /// <summary>�����ݒ�</summary>
    public void InitialSet()
    {
        //�ǃI�u�W�F�N�g��ScaleSize������
        _wallObjSize = _wallObj.transform.localScale.x;
        SetWall();
        StartDig();
        SetTeleportPos(TESTTELEPORT);
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
        map.ObjTransform = obj.transform;
    }
    /// <summary>�G�̐���</summary>
    void CreateEnemy()
    {
        for (int i = 0; i < _scriptableObject.EnemyObject.Count; i++)
        {
            //Enemy�̏o��
            int enemyRnd = new System.Random().Next(0, _scriptableObject.EnemyObject.Count);//�G�̎��
            _scriptableObject.EnemyObject.RemoveAt(enemyRnd);//�o�������G��List����폜
            int random = new System.Random().Next(0, GetFloar().Count);//���̃����_���ȏꏊ�����߂�
            GameObject enemy = Instantiate(_scriptableObject.EnemyObject[enemyRnd]);
            enemy.transform.position = new Vector2(GetFloar()[random].Id % _scriptableObject.MapHorSide - _scriptableObject.MapHorSide / 2,
                GetFloar()[random].Id / _scriptableObject.MapHorSide - _scriptableObject.MapVerSide / 2) * _wallObjSize;
            GetFloar().RemoveAt(random);
        }


    }
    /// <summary>���I�u�W�F�N�g��List��Ԃ�</summary>
    /// <returns>���I�u�W�F�N�g��List</returns>
    List<Map> GetFloar()
    {
        List<Map> generatablePosList = new List<Map>();//���̐�
        foreach (var floar in _map)
        {
            if (floar.State == MapState.Floar)//Floar�̏ꏊ��ۑ�
            {
                generatablePosList.Add(floar);
            }
        }
        return generatablePosList;
    }
    /// <summary>Player�����̏ꏊ�����߂�</summary>
    public Transform DecisionPlayerPos()
    {
        int rndId = new System.Random().Next(0, GetFloar().Count);//���I�u�W�F�N�g�̓����Ă���List�̗v�f�����烉���_���Ȓl���擾
        Map rndMap = GetFloar()[rndId];//���I�u�W�F�N�g�̃����_���ȃI�u�W�F�N�g���擾
        foreach (var item in _map)
        {
            if (item.Id + _scriptableObject.MapHorSide == rndMap.Id)
            {
                if (item.State == MapState.Wall)
                {
                    rndMap.State = MapState.Player;
                    return rndMap.ObjTransform;
                }
            }
        }
        Debug.Log($"rndId:{rndId},�����Id{_map[(rndId + _scriptableObject.MapHorSide) % _scriptableObject.MapHorSide, (rndId + _scriptableObject.MapHorSide) / _scriptableObject.MapHorSide].Id}");
        return DecisionPlayerPos();
    }
    void SetTeleportPos(GameObject[] teleporterObj)
    {
        if (TESTTELEPORT != null)
        {
            return;
        }
        List<Map> leftMapsList = new List<Map>();
        List<Map> rightMapsList = new List<Map>();
        int leftLine = _scriptableObject.MapHorSide / 3; //3�����������̍��̐�
        int rightLine = _scriptableObject.MapHorSide / 3 * 2;//3���������̉E�̐�
        foreach (var item in GetFloar())
        {
            if (item.Id % _scriptableObject.MapHorSide <= leftLine)
            {
                leftMapsList.Add(item);
            }
            if (item.Id % _scriptableObject.MapHorSide >= rightLine)
            {
                rightMapsList.Add(item);
            }
        }
        int leftRnd = new System.Random().Next(0, leftMapsList.Count);
        int rightRnd = new System.Random().Next(0, rightMapsList.Count);
        var leftTele = Instantiate(TESTTELEPORT[0]);
        var rightTele = Instantiate(TESTTELEPORT[1]);
        leftTele.transform.position = leftMapsList[leftRnd].ObjTransform.position;
        rightTele.transform.position = rightMapsList[rightRnd].ObjTransform.position;
    }
    class Map
    {
        MapState _state = MapState.Wall;
        public MapState State { get => _state; set => _state = value; }
        Transform _objTransform;
        public Transform ObjTransform { get => _objTransform; set => _objTransform = value; }
        public readonly int Id;
        public Map(int id)
        {
            Id = id;
        }
    }
}
