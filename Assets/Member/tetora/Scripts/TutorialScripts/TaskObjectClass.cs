using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObjectClass : MonoBehaviour
{
    [SerializeField]
    int _taskId;
    int _createCount = 0;
    private void Start()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TutorialUIManager.Instance.ChangeTaskText(_taskId);
            if (_taskId == 3)
            {
                if (_createCount >= 10)
                {
                    return;
                }
                _createCount++;
                TutorialFieldManager.Instance.CreateEnemy();
            }
        }
    }
}
