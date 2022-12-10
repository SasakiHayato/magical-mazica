using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CreateBossStage : MapCreaterBase
{
    [SerializeField]
    GameObject _bossObj;
    [SerializeField]
    Grid _parentGrid;
    [SerializeField]
    List<BossStageScriptable> _stageTipList;
    [SerializeField]
    Transform _createMapPos;//��������ꏊ
    [SerializeField]
    Transform _playerPos;
    [SerializeField]
    Transform _bossPos;
    [SerializeField]
    int _moveSize = 18;

    int _dataNum;//���f�[�^�������
    int _createdNum;//���}�b�v���������
    List<GameObject> _createdStageList = new List<GameObject>();

    public static CreateBossStage Instance;
    public override Transform PlayerTransform { get; protected set; }
    public int CreatedNum { get => _createdNum; set => _createdNum = value; }

    //private void Awake()
    //{
    //    Instance = this;
    //    InitialSet();
    //}
    protected override void Create()
    {
        Instance = this;
        InitialSet();
    }
    protected override void Initalize()
    {
        if (_parentGrid != null)
        {
            foreach (Transform item in _parentGrid.transform)
            {
                Destroy(item.gameObject);
            }
        }
    }
    /// <summary>
    /// �ŏ��ɂ��邱��
    /// </summary>
    public void InitialSet()
    {
        PlayerTransform = _playerPos;
        CreateMap();
        CreateBoss();
    }

    /// <summary>
    /// �}�b�v����
    /// </summary>
    public void CreateMap()
    {
        _dataNum++;
        int rnd = new System.Random().Next(0, _stageTipList.Count);//�f�[�^�Q�̐�����K���Ȓl���擾
        GameObject parentObj = new GameObject();
        _createdStageList.Add(parentObj);
        parentObj.name = $"Stage:{_dataNum}";
        parentObj.transform.SetParent(_parentGrid.transform);
        for (int i = 0; i < _stageTipList[rnd].StageList.Count; i++)
        {
            GameObject stage = Instantiate(_stageTipList[rnd].StageList[i].gameObject, parentObj.transform);
            _createdNum++;
            SetMapTip(stage);
        }
    }
    /// <summary>
    /// �X�e�[�W�̏ꏊ�ύX
    /// </summary>
    /// <param name="mapTip"></param>
    void SetMapTip(GameObject mapTip)
    {
        mapTip.transform.position =
            new Vector2(_createMapPos.position.x - _moveSize, _createMapPos.position.y);
        _createMapPos = mapTip.transform;
    }
    /// <summary>
    /// ����Ȃ��Ȃ����}�b�v�̍폜
    /// </summary>
    public void DestroyMap()
    {
        Destroy(_createdStageList.First());
        _createdStageList.RemoveAt(0);
    }
    public int CreateCount()
    {
        if (_dataNum == 1)
        {
            return CreatedNum - 1;
        }
        return CreatedNum;
    }
    /// <summary>
    /// Boss�̐���
    /// </summary>
    public void CreateBoss()
    {
        GameObject boss = Instantiate(_bossObj);
        boss.transform.position = _bossPos.position;
        DebugSetEnemyObject.SetEnemy(boss.transform);
    }
}





