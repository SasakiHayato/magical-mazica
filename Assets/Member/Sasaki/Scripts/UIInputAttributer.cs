using UnityEngine;

public class UIInputAttributer : MonoBehaviour
{
    readonly string EnemyTag = "Enemy";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(EnemyTag)) return;
        InputSetting.UIInputOperate.IsInputAttribute = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(EnemyTag)) return;
        InputSetting.UIInputOperate.IsInputAttribute = true;
    }
}
