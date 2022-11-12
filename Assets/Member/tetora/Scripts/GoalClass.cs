using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalClass : MonoBehaviour
{
    [SerializeField]
    float _goalDis = 1.0f;

    private void Start()
    {
        SetCollisionDetection();
    }
    /// <summary>�����蔻��͈̔͐ݒ�</summary>
    void SetCollisionDetection()
    {
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = _goalDis;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�S�[������");
            //�}�b�v���ړ�����֐��������ŌĂ�
        }
    }
}
