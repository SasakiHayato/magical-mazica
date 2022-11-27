using UnityEngine;

/// <summary>
/// ������Ȃ���
/// </summary>
public static class RigidMasterData
{
    public enum ImpulseDirectionType
    {
        Vertical,
        Horizontal,
    }

    /// <summary>
    /// �d�͂̋���
    /// </summary>
    public static float GravityScale { get; private set; } = 10;
    /// <summary>
    /// �d�͒萔
    /// </summary>
    public readonly static float Gravity = Physics2D.gravity.y;
    /// <summary>
    /// �����萔
    /// </summary>
    public readonly static float InertiaRate = 0.8f;
}