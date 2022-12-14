using UnityEngine;

/// <summary>
/// いじらないで
/// </summary>
public static class RigidMasterData
{
    public enum ImpulseDirectionType
    {
        Vertical,
        Horizontal,
    }

    /// <summary>
    /// 重力の強さ
    /// </summary>
    public static float GravityScale { get; private set; } = 10;
    /// <summary>
    /// 重力定数
    /// </summary>
    public readonly static float Gravity = Physics2D.gravity.y;
    /// <summary>
    /// 慣性定数
    /// </summary>
    public readonly static float InertiaRate = 0.8f;
}