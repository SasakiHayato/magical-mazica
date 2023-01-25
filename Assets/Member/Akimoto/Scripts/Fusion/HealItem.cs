using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class HealItem : DropObjectBase
{
    [SerializeField] int _healValue;
    /// <summary>‰ñ•œ—Ê</summary>
    public int HealValue => _healValue;

    public static HealItem Init(HealItem original, Vector2 createPosition, Player player = null)
    {
        HealItem ret = Instantiate(original, createPosition, Quaternion.identity);
        ret.Setup(player);
        return ret;
    }

    private void Setup(Player player)
    {
        SubscribeApproachingEvent(player.gameObject);
        //_approachingDropObject.SetAction(player.‰ñ•œ‚³‚¹‚é);
    }
}
