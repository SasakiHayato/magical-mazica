using UnityEngine;

/// <summary>
/// 地面を感知するためのデータ
/// </summary>

public class GroundData : MonoBehaviour
{
    readonly string GroundTag = "";

    /// <summary>
    /// 地面との接地判定
    /// </summary>
    public bool IsGround { get; private set; }

    void Awake()
    {
        IsGround = false;
    }

    void CheckGround(bool isGround, string tagName)
    {
        if (GroundTag == "")
        {
            Debug.LogWarning("GroundTagが設定せれていません");
            IsGround = isGround;

            return;
        }

        if (tagName == GroundTag)
        {
            IsGround = isGround;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckGround(true, collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckGround(false, collision.tag);
    }
}
