using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    List<Task> _taskList;
    public List<Task> TaskList { get => _taskList; set => _taskList = value; }
    public static TaskManager Instance { get; private set; }
    private void Start()
    {
        Instance = this;
    }
}
[System.Serializable]
public class Task
{
    [SerializeField]
    string _taskText;
    [SerializeField, TextArea]
    string _inputText;
    [SerializeField]
    int _id;

    public string TaskText { get => _taskText; }
    public string InputText { get => _inputText; }
    public int Id { get => _id; }
}
