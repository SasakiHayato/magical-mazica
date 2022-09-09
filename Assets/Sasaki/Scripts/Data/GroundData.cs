using UnityEngine;

/// <summary>
/// �n�ʂ����m���邽�߂̃f�[�^
/// </summary>

public class GroundData : MonoBehaviour
{
    readonly string GroundTag = "";

    /// <summary>
    /// �n�ʂƂ̐ڒn����
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
            Debug.LogWarning("GroundTag���ݒ肹��Ă��܂���");
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
