using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    Player _player;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject go = collision.transform.root.gameObject;
            go.GetComponent<IDamagable>().AddDamage(_player.Damage);
        }
    }
}
