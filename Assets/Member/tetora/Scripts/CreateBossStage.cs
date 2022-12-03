using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CreateBossStage : MapCreaterBase
{
    [SerializeField]
    Grid _parentGrid;
    [SerializeField]
    List<BossStageScriptable> _stageTipList;    
    [SerializeField]
    Transform _createMapPos;//��������ꏊ
    [SerializeField]
    int _moveSize = 18;

    List<GameObject> _createdStageList = new List<GameObject>();
    bool _isCreated = false;//true:���� false:���Ȃ�
    public static CreateBossStage Instance;

    public override Transform PlayerTransform { get; protected set; }
    public bool IsCreated { get => _isCreated; set => _isCreated = value; }

    private void Start()
    {
        Instance = this;
        InitialSet();
    }
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
        CreateMap();
    }
    /// <summary>
    /// �}�b�v����
    /// </summary>
    public void CreateMap()
    {
        int rnd = new System.Random().Next(0, _stageTipList.Count);//�f�[�^�Q�̐�����K���Ȓl���擾
        if (_stageTipList[rnd].FirstCreatable)
        {
            for (int i = 0; i < _stageTipList[rnd].StageList.Count; i++)
            {
                GameObject stage = Instantiate(_stageTipList[rnd].StageList[i].gameObject, _parentGrid.transform);
                _createdStageList.Add(stage);
                SetMapTip(stage);
            }
        }
        CreateMap();
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
    void DestroyMap()
    {        
        Destroy(_createdStageList.First());
        _createdStageList.RemoveAt(0);//�擪�̃I�u�W�F�N�g���폜
    }
}





