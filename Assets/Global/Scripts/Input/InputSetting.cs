using System.Collections.Generic;
using UnityEngine;

public class InputSetting
{
    InputUserType _currentUser = InputUserType.Player;

    List<ButtonInputData> _buttonInputDataList = new List<ButtonInputData>();
    List<AxisInputData> _axisInputDataList = new List<AxisInputData>();

    static InputSetting s_instance = null;

    void Run()
    {
        _buttonInputDataList.ForEach(b => 
        {
            if (b.InputUserType != _currentUser || !SetInput(b.InputType, b.Path)) return;
            
            b.Action.Invoke();
        });

        _axisInputDataList.ForEach(b => 
        {
            if (b.InputUserType != _currentUser) return;

            float x = Input.GetAxisRaw(b.HorizontalPath);
            float y = Input.GetAxisRaw(b.VerticalPath);
            
            b.Action.Invoke(new Vector2(x, y));
        });
    }

    bool SetInput(InputType inputType, string path)
    {
        switch (inputType)
        {
            case InputType.Down: return Input.GetButtonDown(path);
            case InputType.Stay: return Input.GetButton(path);
            case InputType.Up: return Input.GetButtonUp(path);
        }

        return false;
    }

    public InputSetting CreateButtonInput(string path, System.Action action, InputUserType userType, InputType input = InputType.Down)
    {
        ButtonInputData inputData = new ButtonInputData(input, path, userType, action);
        _buttonInputDataList.Add(inputData);

        return this;
    }

    public InputSetting CreateAxisInput(string horizontal, string vertical, InputUserType userType, System.Action<Vector2> action)
    {
        AxisInputData inputData = new AxisInputData(horizontal, vertical, userType, action);
        _axisInputDataList.Add(inputData);

        return this;
    }

    public static void ChangeInputUser(InputUserType userType)
    {
        s_instance._currentUser = userType;
    }

    public static void SetInputUser(GameObject user, out InputSetting inputOperator)
    {
        if (s_instance == null)
        {
            inputOperator = new InputSetting();
            s_instance = inputOperator;
        }
        else
        {
            inputOperator = s_instance;
        }

        user.AddComponent<InputOperator>().Setup(s_instance.Run);
    }

    public static void Dispose()
    {
        s_instance._axisInputDataList = new List<AxisInputData>();
        s_instance._buttonInputDataList = new List<ButtonInputData>();
    }
}
