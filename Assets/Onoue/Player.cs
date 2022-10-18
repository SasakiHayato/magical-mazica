using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Player : MonoBehaviour
{
    ReactiveProperty<int> _hp;
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    Rigidbody2D _rb;
    bool _isGrounded;
    Animator _anim;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Attack");
        }
    }
    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    public void PlayerMove(Vector2 dir)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded= false;
    }
}
