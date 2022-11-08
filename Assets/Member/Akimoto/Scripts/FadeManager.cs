using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// フェード管理クラス
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] List<FadeSetting> _settings;
    public bool IsAnim { get; private set; }

    public void Setup()
    {

    }

    /// <summary>
    /// フェードアニメーションの再生
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="fadeType"></param>
    /// <returns></returns>
    public async UniTask PlayAnimation(FadeAnimationType animationType, FadeType fadeType)
    {
        IsAnim = true;
        float time = default;
        foreach (var s in _settings)
        {
            if (animationType == s.FadeAnimationType)
            {
                time = s.Play(fadeType);
            }
        }
        time = time * 1000; //ミリ秒に変換
        await UniTask.Delay((int)time);
        IsAnim = false;
    }

    /// <summary>
    /// フェードのアニメーション設定を行えるようにするクラス
    /// </summary>
    [System.Serializable]
    public class FadeSetting
    {
        [SerializeField] FadeAnimationType _fadeAnimationType;
        [SerializeField] FadeType _fadeType;
        [SerializeField] Animation _fadeinAnim;
        [SerializeField] Animation _fadeoutAnim;
        /// <summary>アニメーションの指定</summary>
        public FadeAnimationType FadeAnimationType => _fadeAnimationType;
        /// <summary>フェードインアウトの指定</summary>
        public FadeType FadeType => _fadeType;
        /// <summary>FadeTypeに合わせたAnimationを再生し、再生秒数を返す</summary>
        public float Play(FadeType fadeType)
        {
            switch (_fadeType)
            {
                case FadeType.In:
                    _fadeinAnim.Play();
                    return _fadeinAnim.clip.length;
                case FadeType.Out:
                    _fadeoutAnim.Play();
                    return _fadeoutAnim.clip.length;
                default:
                    throw new System.Exception("想定されていないパラメーターが渡されました");
            }
        }
    }
}

/// <summary>
/// フェードのアニメーション
/// </summary>
public enum FadeAnimationType
{
    Normal,
}

/// <summary>
/// フェードインアウト
/// </summary>
public enum FadeType
{
    In,
    Out,
}
