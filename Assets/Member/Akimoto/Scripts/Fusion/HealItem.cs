using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// ‰ñ•œƒAƒCƒeƒ€
/// </summary>
public class HealItem : DropObjectBase
{
    /// <summary>‰ñ•œ—Ê</summary>
    [SerializeField] int _healValue;

    public static HealItem Init(HealItem original, Vector2 createPosition, Player player = null)
    {
        HealItem ret = Instantiate(original, createPosition, Quaternion.identity);
        ret.Setup(player);
        return ret;
    }

    private void Setup(Player player)
    {
        SubscribeApproachingEvent(player.gameObject);
        _approachingDropObject.SetAction = () => player.Heal(_healValue);
        Destroy(gameObject);
    }
}
