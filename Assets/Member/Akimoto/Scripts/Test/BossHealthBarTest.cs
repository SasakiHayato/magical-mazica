using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BossHealthBarTest : MonoBehaviour
{
    [SerializeField] int _maxhp = 100;
    private ReactiveProperty<int> _hp = new ReactiveProperty<int>();

    private async void Start()
    {
        _hp.Value = _maxhp;
        GUIManager.ShowBossHealthBar(_maxhp, _hp, this);
        Debug.Log("2•b‘Ò‹@");
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _hp.Value -= 1;
        Debug.Log("1Œ¸‚ç‚µ‚½");
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _hp.Value -= 10;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _hp.Value -= 50;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        _hp.Value = 0;
        await UniTask.Delay(System.TimeSpan.FromSeconds(2));
        GUIManager.DisableBossHealBar();
    }
}
