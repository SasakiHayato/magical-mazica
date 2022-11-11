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

    float _wallObjSize = 3;//�}�b�v���̃T�C�Y
    StageMap _stageMap;
    int _startDigPos;//�@��n�߂�n�_
    public Transform PlayerTransform { get; private set; }
    /// <summary>�����ݒ�</summary>
    public void InitialSet()
    {
        _stageMap = new StageMap(_scriptableObject.MapHorSide, _scriptableObject.MapVerSide);
        //�ǃI�u�W�F�N�g��ScaleSize������
        _wallObjSize = _wallObj.transform.localScale.x;
        StartDig();
        DecisionPlayerPos();
        InstantiateEnemy();
    }
    /// <summary>�����_���ȊJ�n�n�_�����߂�</summary>
    int RandomPos()
    {
        int rndX = new System.Random().Next(1, (_stageMap.MaxX - 1) / 2) * 2 + 1;
        int rndY = new System.Random().Next(1, (_stageMap.MaxY - 1) / 2) * 2 + 1;
        return _startDigPos = rndX + rndY * _stageMap.MaxX;
    }
    /// <summary>4�����������_���ȏ��ԂŕԂ�</summary>
    /// <param name="id">�J�n�n�_��id</param>
    /// <returns>4������id</returns>
    IEnumerable<(int twoTarget, int oneTarget)> CheckDir(int id)
    {
        (int twoTarget, int oneTarget)[] twoTargetDirs = {
            (id - (_stageMap.MaxX  * 2),id - _stageMap.MaxX ), //��
            (id + _stageMap.MaxX  * 2,id + _stageMap.MaxX ), //��
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
            if (dir.two < 0 || dir.two > _stageMap.Length - 1)//�͈͓���id�̂ݒl��Ԃ�
            {
                continue;
            }
            //���̃T�C�Y�𒴂��Ȃ���
            if (id % _stageMap.MaxX < 1 || id % _stageMap.MaxX >= _stageMap.MaxX - 1)
            {
                continue;
            }
            //2�悪�ǂ��m�F
            if (_stageMap[dir.two].State != MapState.Wall)
            {
                continue;
            }
            //1�悪�ǂ��m�F
            if (_stageMap[dir.one].State != MapState.Wall)
            {
                continue;
            }
            if (dir.two / _stageMap.MaxX == 0 || dir.two / _stageMap.MaxX == _stageMap.MaxY - 1)
            {
                continue;
            }
            if (dir.two % _stageMap.MaxX == 0 || dir.two % _stageMap.MaxX == _stageMap.MaxX - 1)
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
        _stageMap[id].State = MapState.Floar;
        _stageMap[id].IsGenerate = true;
        foreach (var posId in CheckDir(id))
        {
            _stageMap[posId.oneTarget].State = MapState.Floar;
            //Debug.Log($"stageMap:{_stageMap[posId.oneTarget].State}");
            Dig(posId.twoTarget);
        }
    }
    /// <summary>�@��n�߂�</summary>
    void StartDig()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _stageMap)
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
    void SetTransform(GameObject obj, Point map)
    {
        obj.transform.SetParent(_parentObj.transform);
        obj.transform.position = _stageMap[map.Id, _wallObjSize];
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
        int random = _stageMap.RandomFloarId();//�����_���ȏ���ID���擾���ϐ��Ɋi�[
        if (_stageMap[random].IsGenerate != true)
        {
            SetEnemyPos(enemy);
        }
        else
        {
            enemy.transform.position = _stageMap[random, _wallObjSize];
            _stageMap[random].IsGenerate = false;
        }
    }
    /// <summary>Player�����̏ꏊ�����߂�</summary>
    public void DecisionPlayerPos()
    {
        int rndId = _stageMap.RandomFloarId();//���I�u�W�F�N�g�̓����Ă���List�̗v�f�����烉���_���Ȓl���擾
                                              // Map rndMap = GetFloar()[rndId];//���I�u�W�F�N�g�̃����_���ȃI�u�W�F�N�g���擾
        if (rndId - _stageMap.MaxX >= 0 && _stageMap[rndId - _stageMap.MaxX].State == MapState.Wall)
        {
            _stageMap[rndId].State = MapState.Player;//Player��������ꏊ
            for (int i = 0; i < _releDis; i++)
            {
                foreach (var point in _stageMap.CheckDir(_stageMap[rndId], i))
                {
                    _stageMap[point].IsGenerate = false;
                }
            }
            PlayerTransform = _stageMap[rndId].ObjTransform;
            return;
        }
        DecisionPlayerPos();
    }

    //void SetTeleportPos(GameObject[] teleporterObj)
    //{
    //    if (TESTTELEPORT != null)
    //    {
    //        return;
    //    }
    //    List<Map> leftMapsList = new List<Map>();
    //    List<Map> rightMapsList = new List<Map>();
    //    int leftLine = _stageMap.maxX  / 3; //3�����������̍��̐�
    //    int rightLine = _stageMap.maxX  / 3 * 2;//3���������̉E�̐�
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
}

