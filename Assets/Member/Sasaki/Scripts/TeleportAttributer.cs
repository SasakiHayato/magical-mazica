using UnityEngine;

public class TeleportAttributer : MonoBehaviour
{
    public bool IsAttribute { get; private set; } = true;

    readonly string EnemyTag = "Enemy";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(EnemyTag)) return;
        IsAttribute = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(EnemyTag)) return;
        IsAttribute = true;
    }
}
