using UnityEngine;

public interface IExecuteIdle
{
    void Setup();
    void OnEnable();
    Vector2 Execute();
    void Initalize();
}

public interface IExecuteMove
{
    void Setup();
    void OnEnable();
    Vector2 Execute();
    void Initalize();
}

public interface IExecuteAttack
{
    void Setup();
    void OnEnable();
    Vector2 Execute();
    void Initalize();
}