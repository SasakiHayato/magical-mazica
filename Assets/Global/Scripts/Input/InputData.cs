using System;
using UniRx;

public enum InputType
{
    Down,
    Stay,
    Up,
}

public enum InputUserType
{
    Player,
    UI,
}

public class ButtonInputData
{
    public ButtonInputData(InputType input, string path, InputUserType userType, Action action)
    {
        InputType = input;
        InputUserType = userType;
        Path = path;
        Action = action;
    }

    public InputType InputType { get; private set; }
    public InputUserType InputUserType { get; private set; }
    public string Path { get; private set; }
    public Action Action { get; private set; }
}

public class AxisInputData
{
    public AxisInputData(string horizontalPath, string verticalPath, InputUserType userType, Action<UnityEngine.Vector2> action)
    {
        InputUserType = userType;
        HorizontalPath = horizontalPath;
        VerticalPath = verticalPath;
        Action = action;
    }

    public InputUserType InputUserType { get; private set; }
    public string HorizontalPath { get; private set; }
    public string VerticalPath { get; private set; }
    public Action<UnityEngine.Vector2> Action { get; private set; }
}
