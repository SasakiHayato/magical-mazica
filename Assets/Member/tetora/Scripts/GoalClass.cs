using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalClass : MonoBehaviour
{
    [SerializeField]
    float _goalDis = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ゴールした");
            //マップを移動する関数をここで呼ぶ
        }
    }
}
