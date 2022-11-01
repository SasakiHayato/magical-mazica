using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class DamageTextTest : MonoBehaviour
{
    [SerializeField] DamageText _damageText;

    private void Start()
    {
        DamageText.Init(_damageText, "50", Vector2.zero, Color.red);
    }
}
