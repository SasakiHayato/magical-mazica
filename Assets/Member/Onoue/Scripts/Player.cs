using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxHP;
    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    Rigidbody2D _rb;
    bool _isGrounded;
    Animator _anim;
    /// <summary>最大HP</summary>
    public int MaxHP => _maxHP;
    public Vector2 Direction { get; set; }
    /// <summary>現在HPの更新の通知</summary>
    public System.IObservable<int> CurrentHP => _hp;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _hp.Value = _maxHP;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    
    /// <summary>
    /// 移動
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
}
