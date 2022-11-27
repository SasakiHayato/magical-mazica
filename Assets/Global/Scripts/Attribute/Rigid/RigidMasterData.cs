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

    /// <summary>
    /// d—Í‚Ì‹­‚³
    /// </summary>
    public static float GravityScale { get; private set; } = 10;
    /// <summary>
    /// d—Í’è”
    /// </summary>
    public readonly static float Gravity = Physics2D.gravity.y;
    /// <summary>
    /// Šµ«’è”
    /// </summary>
    public readonly static float InertiaRate = 0.8f;
}