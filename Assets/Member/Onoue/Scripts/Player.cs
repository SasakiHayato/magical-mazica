using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int _maxHP;
    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] int _damage = 5;
    Rigidbody2D _rb;
    bool _isGrounded;
    Animator _anim;
    FusionItem _fusionItem;
    /// <summary>�U����</summary>
    public int Damage { get => _damage; set { } }
    /// <summary>�ő�HP</summary>
    public int MaxHP => _maxHP;
    public Vector2 Direction { get; set; }
    /// <summary>����HP�̍X�V�̒ʒm</summary>
    public System.IObservable<int> CurrentHP => _hp;


    private void Start()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _anim);
        _fusionItem = FindObjectOfType<FusionItem>();
        _hp.Value = _maxHP;
    }

    /// <summary>
    /// �U��
    /// </summary>
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
    /// <summary>
    /// �������U��
    /// </summary>
    public void Fire()
    {
        _fusionItem.Attack(transform.position);
    }

    /// <summary>
    /// �W�����v
    /// </summary>
    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    /// <param name="dir"></param>
    private void PlayerMove(Vector2 dir)
    {
        //float h = Input.GetAxisRaw("Horizontal") * _speed;
        Vector2 velocity = new Vector2(dir.x * _speed, _rb.velocity.y);
        _rb.velocity = velocity;
        if (dir.x != 0)
        {
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerMove(Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }

    public void AddDamage(int damage)
    {
        _hp.Value -= damage;
    }
}
