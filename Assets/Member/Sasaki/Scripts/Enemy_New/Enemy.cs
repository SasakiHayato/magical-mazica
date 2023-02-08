using UnityEngine;

public class Enemy : EnemyBase, IDamageForceble, IInputEventable
{
    [SerializeField] int _attackIsActiveFrame;
    [SerializeField] int _attackEndActiveFrame;
    [SerializeField] AnimOperator _animOperator;
    [SerializeField] BehaviourTree.BehaviourTreeUser _treeUser;
    [SerializeField] FusionMaterialObject _materialObject;
    [SerializeField] HealItem _healItem;
    [SerializeField] RawMaterialData _materialData;
    [SerializeField] float materialDropPositionRange;
    [SerializeField] ItemDropDetailsSettings _itemDropDetailsSettings;

    System.Action<GameObject> _deadCallback = null;
    public System.Action<GameObject> SetDeadCallback { set { _deadCallback = value; } }

    protected override void Setup()
    {
        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateRun())
            .AddState(State.Attack, new EnemyStateAttack())
            .AddState(State.KnockBack, new EnemyStateKnockBack());

        EnemyStateData.AttackAciveFrame = (_attackIsActiveFrame, _attackEndActiveFrame);
        MonoState
            .AddMonoData(_animOperator)
            .AddMonoData(_treeUser);

        MonoState.IsRun = true;

        if (GameController.Instance.OnInputEvent)
        {
            gameObject.GetComponent<IInputEventable>().OnEvent();
        }
    }

    protected override void Execute()
    {
        Rigid.SetMoveDirection = MoveDirection * Speed;
    }
    protected override void DeadEvent()
    {
        _deadCallback.Invoke(gameObject);
        RawMaterialDatabase rawMaterial = _materialData.GetMaterialDataRandom();

        Player player = GameController.Instance.Player.GetComponent<Player>();

        //�f�ރA�C�e���̃h���b�v
        for (int i = 0; i < _itemDropDetailsSettings.GetRandomDropNum; i++)
        {
            FusionMaterialObject.Init(_materialObject, transform.position, materialDropPositionRange, rawMaterial, player);
        }
        //�񕜃A�C�e���̃h���b�v
        if (_itemDropDetailsSettings.GetHealItemLottery)
        {
            HealItem.Init(_healItem, transform.position);
        }

        base.DeadEvent();
    }

    protected override bool IsDamage(int damage)
    {
        EffectStocker.Instance.LoadEffect("Damage", transform.position);
        return true;
    }

    void IDamageForceble.OnFoece(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        Rigid.SetImpulse(direction.x, RigidMasterData.ImpulseDirectionType.Horizontal, true);
        Rigid.SetImpulse(direction.y, RigidMasterData.ImpulseDirectionType.Vertical, true);

        MonoState.ChangeState(State.KnockBack);
    }

    void IInputEventable.OnEvent()
    {
        _treeUser.SetRunRequest(false);
        GetComponent<IBehaviourDatable>().SetMoveDirection = Vector2.zero;
    }

    void IInputEventable.DisposeEvent()
    {
        _treeUser.SetRunRequest(true);
    }

    /// <summary>
    /// �f�ނ�񕜃A�C�e���̃h���b�v���̒���
    /// </summary>
    [System.Serializable]
    public class ItemDropDetailsSettings
    {
        [SerializeField, Range(0, 100)] float _healItemDropChance;
        [SerializeField] int _materialDropMinNum;
        [SerializeField] int _materialDropMaxNum;
        /// <summary>�񕜃A�C�e�����h���b�v���邩�ǂ����̒��I</summary>
        public bool GetHealItemLottery
        {
            get
            {
                int r = Random.Range(0, 100);
                if (r < _healItemDropChance)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>�f�ރA�C�e���̃h���b�v���𒊑I</summary>
        public int GetRandomDropNum => Random.Range(_materialDropMinNum, _materialDropMaxNum);
    }
}