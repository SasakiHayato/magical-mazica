using UnityEngine;

/// <summary>
/// ‚¢‚¶‚ç‚È‚¢‚Å
/// </summary>
public static class RigidMasterData
{
    public enum ImpulseDirectionType
    {
        Vertical,
        Horizontal,
    }

    public static float GravityScale { get; private set; } = 10;

    public readonly static float Gravity = Physics2D.gravity.y;
    public readonly static float InertiaRate = 0.8f;
}