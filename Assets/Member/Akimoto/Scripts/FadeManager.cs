using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{


    public void Setup()
    {

    }

    public class FadeSetting
    {
        [SerializeField] FadeAnimationType _fadeAnimationType;
        [SerializeField] FadeType _fadeTyp;
        [SerializeField] AnimationClip _clip;
        public FadeAnimationType FadeAnimationType => _fadeAnimationType;
        public FadeType FadeType => _fadeTyp;
        public AnimationClip Clip => _clip;
    }
}

/// <summary>
/// フェードのアニメーション指定
/// </summary>
public enum FadeAnimationType
{

}

/// <summary>
/// フェードインアウト
/// </summary>
public enum FadeType
{
    In,
    Out,
}
