using UnityEngine;

public class TeleportAttributer : MonoBehaviour
{
    public bool IsAttribute { get; private set; } = true;

    readonly string PlayerTag = "Player";
    readonly string EnemyTag = "Enemy";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            InputSetting.UIInputOperate.IsInputAttribute = true;
        }

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            IsAttribute = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            InputSetting.UIInputOperate.IsInputAttribute = false;
        }

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            IsAttribute = true;
        }
    }
}
