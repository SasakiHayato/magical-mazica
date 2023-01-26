using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class HealItemTest : MonoBehaviour
{
    [SerializeField] HealItem _healItem;

    private void Start()
    {
        if (Input.GetKey("K"))
        {
            HealItem.Init(_healItem, transform.position, GameObject.Find("Player(Clone)").GetComponent<Player>());
        }
    }
}
