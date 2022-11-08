using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �t�F�[�h�Ǘ��N���X
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] List<FadeSetting> _settings;
    public bool IsAnim { get; private set; }

    public void Setup()
    {

    }

    /// <summary>
    /// �t�F�[�h�A�j���[�V�����̍Đ�
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
        time = time * 1000; //�~���b�ɕϊ�
        await UniTask.Delay((int)time);
        IsAnim = false;
    }

    /// <summary>
    /// �t�F�[�h�̃A�j���[�V�����ݒ���s����悤�ɂ���N���X
    /// </summary>
    [System.Serializable]
    public class FadeSetting
    {
        [SerializeField] FadeAnimationType _fadeAnimationType;
        [SerializeField] FadeType _fadeType;
        [SerializeField] Animation _fadeinAnim;
        [SerializeField] Animation _fadeoutAnim;
        /// <summary>�A�j���[�V�����̎w��</summary>
        public FadeAnimationType FadeAnimationType => _fadeAnimationType;
        /// <summary>�t�F�[�h�C���A�E�g�̎w��</summary>
        public FadeType FadeType => _fadeType;
        /// <summary>FadeType�ɍ��킹��Animation���Đ����A�Đ��b����Ԃ�</summary>
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
                    throw new System.Exception("�z�肳��Ă��Ȃ��p�����[�^�[���n����܂���");
            }
        }
    }
}

/// <summary>
/// �t�F�[�h�̃A�j���[�V����
/// </summary>
public enum FadeAnimationType
{
    Normal,
}

/// <summary>
/// �t�F�[�h�C���A�E�g
/// </summary>
public enum FadeType
{
    In,
    Out,
}
