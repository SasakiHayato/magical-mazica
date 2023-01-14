using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObjectClass : MonoBehaviour
{
    [SerializeField]
    int _taskId;

    bool _isTask;
    private void Start()
    {
        _isTask = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!_isTask)
        //{
        //    return;
        //}
        if (collision.CompareTag("Player"))
        {
            TutorialUIManager.Instance.ChangeTaskText(_taskId);
            if (_taskId == 3)
            {
                TutorialFieldManager.Instance.CreateEnemy();
            }
            _isTask = false;
        }
    }
}
