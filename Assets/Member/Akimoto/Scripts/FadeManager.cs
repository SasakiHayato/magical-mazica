using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using DG.Tweening;
using System.Threading;

/// <summary>
/// フェード管理クラス
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] List<FadeSetting> _settings;
    /// <summary>アニメーション中フラグ</summary>
    public bool IsAnim { get; private set; }

    public void Setup()
    {
        //画像全部非アクティブにする
        foreach (var s in _settings)
        {
            s.AllSetActive(false);
        }
    }

    /// <summary>
    /// フェードアニメーションの再生<br/>
    /// awaitで待つことで非同期で使うことが出来る
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="fadeType"></param>
    /// <returns></returns>
    public async UniTask PlayAnimation(FadeAnimationType animationType, FadeType fadeType, CancellationToken cancellationToken = default)
    {
        IsAnim = true;
        //渡されたanimationTypeと一致するものを探して再生
        foreach (var s in _settings)
        {
            if (animationType == s.FadeAnimationType)
            {
                await s.Play(fadeType, cancellationToken);
                break;
            }
        }
        IsAnim = false;
    }

    /// <summary>
    /// フェードのアニメーション設定を行えるようにするクラス
    /// </summary>
    [System.Serializable]
    public class FadeSetting
    {
        [SerializeField] FadeAnimationType _fadeAnimationType;
        [SerializeField] Animator _fadeinAnimatorObject;
        [SerializeField] Animator _fadeoutAnimatorObject;
        /// <summary>アニメーションの指定</summary>
        public FadeAnimationType FadeAnimationType => _fadeAnimationType;
        /// <summary>FadeTypeに合わせたAnimationを再生し、再生秒数を返す</summary>
        public async UniTask Play(FadeType fadeType, CancellationToken cancellationToken = default)
        {
            float time = default;
            switch (fadeType)
            {
                case FadeType.In:
                    _fadeinAnimatorObject.gameObject.SetActive(true);
                    time = _fadeinAnimatorObject.GetCurrentAnimatorStateInfo(0).length;
                    break;
                case FadeType.Out:
                    _fadeoutAnimatorObject.gameObject.SetActive(true);
                    time = _fadeoutAnimatorObject.GetCurrentAnimatorStateInfo(0).length;
                    break;
                default:
                    throw new System.Exception("想定されていないパラメーターが渡されました");
            }
            await UniTask.Delay(System.TimeSpan.FromSeconds(time), false, PlayerLoopTiming.Update, cancellationToken); //ミリ秒に変換
            AllSetActive(false);
        }

        public void AllSetActive(bool value)
        {
            _fadeinAnimatorObject.gameObject.SetActive(value);
            _fadeoutAnimatorObject.gameObject.SetActive(value);
        }
    }
}

/// <summary>
/// フェードのアニメーション
/// </summary>
public enum FadeAnimationType
{
    Sinple,
    Wave,
}

/// <summary>
/// 画像のフェードインアウト
/// </summary>
public enum FadeType
{
    In,
    Out,
}
