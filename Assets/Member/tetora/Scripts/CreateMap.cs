using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Wall, Floar, Player, Teleport, Goal, Enemy
}
public enum WallType//���̈ʒu�����O�̗R��
{
    None, NonFloar,
    Top, Right, Bottom, Left,//�����������
    TopRight, TopLeft, TopBottom,//�����������
    BottomLeft, BottomRight,
    RightLeft,
    NonTop, NonRight, NonBottom, NonLeft//�O������
}
[System.Serializable]
public class CreateMap : MapCreaterBase
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
    int _releDis = 3;//���}�X������
    TeleporterController _teleporterController;
    [SerializeField]
    int _createEnemyTime = 30;//�G�����̃C���^�[�o��

    float _wallObjSize = 3;//�}�b�v���̃T�C�Y
    GameObject[] _enemies;
    List<GameObject> _stageObjList = new List<GameObject>();
    Dictionary<int, EnemyTransform> _enemyDic = new Dictionary<int, EnemyTransform>();//Key:ID Value:EnemyTransform
    StageMap _stageMap;
    int _startDigPos;//�@��n�߂�n�_

    public StageMap StageMap { get => _stageMap; }

    public static int TepoatObjLength => Instance._teleportObj.Length;

    public static CreateMap Instance { get; set; }

    public override Transform PlayerTransform { get; protected set; }

    protected override void Create()
    {
        Instance = this;
        InitialSet();
    }

    protected override void Initalize()
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
        _enemies = new GameObject[_scriptableObject.EnemyObject.Count];
        _enemyDic = new Dictionary<int, EnemyTransform>();
        for (int i = 0; i < _scriptableObject.EnemyObject.Count; i++)
        {
            _enemies[i] = _scriptableObject.EnemyObject[i].gameObject;
        }
        _teleporterController = new TeleporterController();
        //�ǃI�u�W�F�N�g��ScaleSize������
        _wallObjSize = _wallObj.transform.localScale.x;
        ResetMapFlag();
        StartDig();
        //CreateStage();
        DecisionPlayerPos();
        SetGoalPos();
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
    ///// <summary>�@��n�߂�</summary>
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
    /// <summary>
    /// Stage�̍쐬�J�n
    /// </summary>
    void CreateStage()
    {
        _startDigPos = RandomPos();
        Dig(_startDigPos);
        foreach (var pos in _stageMap)
        {
            if (pos.State == MapState.Floar)
            {
                var emptyObj = new GameObject();
                emptyObj.name = "Floar";
                _stageObjList.Add(emptyObj);
                SetTransform(emptyObj, pos);
                continue;
            }
            else
            {
                CheckAroundState(pos);
                CreateWallObj(pos);
            }
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
            GameObject enemy = Instantiate(_enemies[i]);
            var enebase = enemy.GetComponent<Enemy>();
            enebase.ID = i;
            _enemyDic.Add(enebase.ID, new EnemyTransform(enemy));
            _enemyDic[enebase.ID].IsCreate = false;
            DebugSetEnemyObject.SetEnemy(enemy.transform);
            SetEnemyPos(enemy, enebase.ID);
        }
    }
    /// <summary>
    /// �G�̐�������ꏊ�����߂�
    /// </summary>
    /// <param name="enemy">�G��GameObject</param>
    void SetEnemyPos(GameObject enemy, int count)
    {
        int random = _stageMap.RandomFloarId();//�����_���ȏ���ID���擾���ϐ��Ɋi�[
        var ene = enemy.GetComponent<Enemy>();
        if (_stageMap[random].IsGenerate != true)
        {
            SetEnemyPos(enemy, count);
        }
        else
        {
            if (_stageMap.CheckUnderDir(_stageMap[random], MapState.Floar))//�������Ȃ灁��
            {
                if (ene.IsInstantiateFloat)
                {
                    enemy.transform.position = _stageMap[random, _wallObjSize];
                    _stageMap[random].State = MapState.Enemy;
                    _stageMap[random].IsGenerate = false;
                    _enemyDic[count].Position = enemy.transform.position;
                }
                else
                {
                    SetEnemyPos(enemy, count);
                }
            }
            else
            {
                enemy.transform.position = _stageMap[random, _wallObjSize];
                _stageMap[random].State = MapState.Enemy;
                _stageMap[random].IsGenerate = false;
                _enemyDic[count].Position = enemy.transform.position;
            }
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
    /// <summary>
    /// �S�[���̈ʒu�𒲐�����
    /// </summary>
    void SetGoalPos()
    {
        var goal = Instantiate(_goalObj);
        goal.transform.SetParent(_parentObj.transform);
        Point goalPoint = Dijkstra.GetGoalPoint(_stageMap);
        goalPoint.State = MapState.Goal;
        goal.transform.position = goalPoint.ObjTransform.position;
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
            teleObj.transform.SetParent(_parentObj.transform);
            _teleporterController.CreateData(teleObj.transform, GetTeleportData);

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
    void ResetMapFlag()
    {
        foreach (var item in _stageMap)
        {
            item.IsGenerate = true;
        }
    }
    void GetTeleportData(int id)
    {
        Transform transform = _teleporterController.GetData(id);
        GameController.Instance.Player.position = transform.position;
    }
    /// <summary>
    /// Enemy�����񂾂Ƃ���Flag��ς���
    /// </summary>
    /// <param name="enemy">����Enemy</param>
    public void DeadEnemy(GameObject enemy)//Enemy�����񂾂Ƃ��ɊO������Ă�
    {
        var ene = enemy.GetComponent<Enemy>();
        ChangeFlag(ene.ID);
        StartCoroutine(nameof(CreateEnemyCoroutine), ene.ID);
    }
    /// <summary>
    /// Flag��؂�ւ���
    /// </summary>
    /// <param name="id">�؂�ւ������ꏊ��ID</param>
    void ChangeFlag(int id)
    {
        if (_enemyDic[id].IsCreate)
        {
            _enemyDic[id].IsCreate = false;
        }
        else
        {
            _enemyDic[id].IsCreate = true;
        }
    }
    /// <summary>
    /// �G�𐶐�����
    /// </summary>
    /// <param name="id">�G��ID</param>
    public void CreateEnemy(int id)
    {
        GameObject enemyObj = Instantiate(_enemies[id]);
        var enebase = enemyObj.GetComponent<EnemyBase>();
        enebase.ID = id;
        enemyObj.transform.position = _enemyDic[id].Position;
        ChangeFlag(id);
    }
    IEnumerator CreateEnemyCoroutine(int id) //�|���ꂽ�Ƃ��ɂ����̏ꏊ�����J�E���g�_�E���J�n
    {
        yield return new WaitForSeconds(_createEnemyTime);
        CreateEnemy(id);
    }
    /// <summary>
    /// 4�����̕ǂ𒲂ׂ�
    /// </summary>
    void CheckAroundState(Point pos)
    {
        if (_stageMap[pos.Id - _stageMap.MaxX].State == MapState.Wall || pos.Id - _stageMap.MaxX <= 0)//�オ��
        {
            AddScore(pos, 1000);
        }
        if (_stageMap[pos.Id + 1].State == MapState.Wall || pos.Id + 1 >= _stageMap.MaxX)//�E����
        {
            AddScore(pos, 100);
        }
        if (_stageMap[pos.Id + _stageMap.MaxX].State == MapState.Wall || pos.Id + _stageMap.MaxX <= 0)//������
        {
            AddScore(pos, 10);
        }
        if (_stageMap[pos.Id - _stageMap.MaxX].State == MapState.Wall || pos.Id - _stageMap.MaxX <= 0)//������
        {
            AddScore(pos, 1);
        }
        JudgeScore(pos);
    }
    /// <summary>
    /// �ݒu����I�u�W�F�N�g��Score�ɉ��Z
    /// </summary>
    /// <param name="pos">�ݒu����I�u�W�F�N�g</param>
    /// <param name="score">�オ���Ȃ�+1000 �E+100 ��+10 ��+1</param>
    void AddScore(Point pos, int score)
    {
        pos.JudgeScore += score;
    }
    void JudgeScore(Point pos)
    {
        switch (pos.JudgeScore)
        {
            case 1110://��A�E�A������
                pos.Type = WallType.NonLeft;
                break;
            case 1101://��A�E�A������
                pos.Type = WallType.NonBottom;
                break;
            case 1100://��A�E����
                pos.Type = WallType.TopRight;
                break;
            case 1011://��A���A������
                pos.Type = WallType.NonRight;
                break;
            case 1010://��A������
                pos.Type = WallType.TopBottom;
                break;
            case 1001://��A������
                pos.Type = WallType.TopLeft;
                break;
            case 1000://�ゾ����
                pos.Type = WallType.Top;
                break;
            case 0111://�E�A���A������
                pos.Type = WallType.NonTop;
                break;
            case 0110://�E�A������
                pos.Type = WallType.BottomRight;
                break;
            case 0101://�E�A������
                pos.Type = WallType.RightLeft;
                break;
            case 0100://�E����
                pos.Type = WallType.Right;
                break;
            case 0011://���A������
                pos.Type = WallType.BottomLeft;
                break;
            case 0010://������
                pos.Type = WallType.Bottom;
                break;
            case 0001://������
                pos.Type = WallType.Left;
                break;
            case 0000:
                pos.Type = WallType.NonFloar;
                break;
        }
    }
    /// <summary>
    /// ���I�u�W�F�N�g�̕ύX
    /// </summary>
    /// <param name="point"></param>
    /// <param name="wallType"></param>
    void CreateWallObj(Point point)
    {
        GameObject wallObj = _scriptableObject.GetParts(point.Type);
        Instantiate(wallObj);
        _stageObjList.Add(wallObj);
        SetTransform(wallObj, point);
    }
}

