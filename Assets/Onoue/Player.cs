using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    private void Update()
    {
        PlayerMove();
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Attack");
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal") * _speed;
        Vector2 velocity = new Vector2(h, _rb.velocity.y);
        _rb.velocity = velocity;
        if (h != 0)
        {
            if (h < 0)
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
