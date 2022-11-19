using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport, Goal
}
[System.Serializable]
public class CreateMap : MonoBehaviour, IGameDisposable, IGameSetupable
{
    [SerializeField]
    FieldScriptableObject _scriptableObject;
    [SerializeField]
    GameObject _wallObj;
    [SerializeField]
    GameObject _goalObj;
    [SerializeField]
    GameObject _parentObj;
    [SerializeField]
    GameObject[] _teleportObj;
    [SerializeField]
    int _mapScale = 14;
    [SerializeField]
    int _releDis = 3;//���}�X������
    TeleporterController _teleporterController;
    [SerializeField]//teleporter�����Z�b�g�u����
    int _teleporterCount = 1;

    float _wallObjSize = 3;//�}�b�v���̃T�C�Y
    List<GameObject> _stageObjList = new List<GameObject>();
    StageMap _stageMap;
    int _startDigPos;//�@��n�߂�n�_
    public Transform PlayerTransform { get; private set; }
    public StageMap StageMap { get => _stageMap; }
    private void Awake()
    {
        GameController.Instance.AddGameSetupable(this);
        GameController.Instance.AddGameDisposable(this);
    }
    public void GameSetup()
    {
        InitialSet();
    }
    public void GameDispose()
    {
        if (_parentObj != null)
        {
            foreach (Transform item in _parentObj.transform)//transform��foreach�ŉ񂷂Ǝq�I�u�W�F�N�g������Ă����
            {
                Destroy(item.gameObject);
            }
        }
    }
    /// <summary>�����ݒ�</summary>
    public void InitialSet()
    {
        _stageMap = new StageMap(_scriptableObject.MapHorSide, _scriptableObject.MapVerSide);
        _teleporterController = new TeleporterController();
        //�ǃI�u�W�F�N�g��ScaleSize������
        _wallObjSize = _wallObj.transform.localScale.x;
        StartDig();
        DecisionPlayerPos();
        InstantiateEnemy();
        InstantiateTeleObj();
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
                _stageObjList.Add(emptyObj);
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                var wall = Instantiate(_wallObj);
                _stageObjList.Add(_wallObj);
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

    void SetGoalPos()
    {
        //�o�H�T���A���S���Y�����g���ăv���C���[�����ԉ������ɐ���
    }
    void InstantiateTeleObj()
    {
        int leftX = _stageMap.MaxX / 3;//3�����������̍��̐�
        int rightX = _stageMap.MaxX / 3 * 2;//3�����������̉E�̐�
        int underY = _stageMap.MaxY / 3;//3�����������̉��̐�
        int overY = _stageMap.MaxY / 3 * 2;//3�����������̏�̐�
        List<Point> leftUpList = new List<Point>();//����̏�List
        List<Point> leftDownList = new List<Point>();//�����̏�List
        List<Point> rightUpList = new List<Point>();//�E��̏�List
        List<Point> rightDownList = new List<Point>();//�E���̏�List
        foreach (var item in _stageMap.GetFloar())
        {
            int itemX = item.Id % _stageMap.MaxX;
            int itemY = item.Id / _stageMap.MaxX;
            if (_stageMap.CheckUnderDir(item, MapState.Wall))//�����ǂ̏���T���Ă�
            {
                if (0 < itemX && itemX <= leftX)//��
                {
                    if (0 < itemY && itemY <= underY)//��
                    {
                        leftDownList.Add(item);
                    }
                    else if (overY < itemY && itemY < _stageMap.MaxY)//��
                    {
                        leftUpList.Add(item);
                    }
                }
                else if (rightX < itemX && itemX < _stageMap.MaxX)//�E
                {
                    if (0 < itemY && itemY <= underY)//��
                    {
                        rightDownList.Add(item);
                    }
                    else if (overY < itemY && itemY < _stageMap.MaxY)//��
                    {
                        rightUpList.Add(item);
                    }
                }
            }
        }
        //�I�u�W�F�N�g����
        for (int i = 0; i < _teleportObj.Length; i++)
        {
            var teleObj = Instantiate(_teleportObj[i]);
            switch (i)
            {
                case 0:
                    teleObj.transform.position = CanSetRandomPos(leftUpList).position;
                    break;
                case 1:
                    teleObj.transform.position = CanSetRandomPos(leftDownList).position;
                    break;
                case 2:
                    teleObj.transform.position = CanSetRandomPos(rightUpList).position;
                    break;
                case 3:
                    teleObj.transform.position = CanSetRandomPos(rightDownList).position;
                    break;
            }
        }
    }
    /// <summary>
    /// �����_���ȏꏊ�̉������Ȃ�Transform��Ԃ�
    /// </summary>
    /// <returns>�I�u�W�F�N�g��Transform</returns>
    Transform CanSetRandomPos(List<Point> points)
    {
        int rnd = new System.Random().Next(0, points.Count);
        points[rnd].State = MapState.Teleport;
        return points[rnd].ObjTransform;
    }
    public void GetTeleportData(int id)
    {
        _teleporterController.GetData(id);
    }
}

