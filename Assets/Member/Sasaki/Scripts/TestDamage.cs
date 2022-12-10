using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [SerializeField] GameObject a_;

    public void A()
    {
        a_.GetComponent<IDamagable>().AddDamage(10);
    }
}
