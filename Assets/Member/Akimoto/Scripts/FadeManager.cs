using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using DG.Tweening;
using System.Threading;

/// <summary>
/// �t�F�[�h�Ǘ��N���X
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] List<FadeSetting> _settings;
    /// <summary>�A�j���[�V�������t���O</summary>
    public bool IsAnim { get; private set; }

    public void Setup()
    {
        //�摜�S����A�N�e�B�u�ɂ���
        foreach (var s in _settings)
        {
            s.AllSetActive(false);
        }
    }

    /// <summary>
    /// �t�F�[�h�A�j���[�V�����̍Đ�<br/>
    /// await�ő҂��ƂŔ񓯊��Ŏg�����Ƃ��o����
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="fadeType"></param>
    /// <returns></returns>
    public async UniTask PlayAnimation(FadeAnimationType animationType, FadeType fadeType, CancellationToken cancellationToken = default)
    {
        IsAnim = true;
        //�n���ꂽanimationType�ƈ�v������̂�T���čĐ�
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
    /// �t�F�[�h�̃A�j���[�V�����ݒ���s����悤�ɂ���N���X
    /// </summary>
    [System.Serializable]
    public class FadeSetting
    {
        [SerializeField] FadeAnimationType _fadeAnimationType;
        [SerializeField] Animator _fadeinAnimatorObject;
        [SerializeField] Animator _fadeoutAnimatorObject;
        /// <summary>�A�j���[�V�����̎w��</summary>
        public FadeAnimationType FadeAnimationType => _fadeAnimationType;
        /// <summary>FadeType�ɍ��킹��Animation���Đ����A�Đ��b����Ԃ�</summary>
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
                    throw new System.Exception("�z�肳��Ă��Ȃ��p�����[�^�[���n����܂���");
            }
            await UniTask.Delay(System.TimeSpan.FromSeconds(time), false, PlayerLoopTiming.Update, cancellationToken); //�~���b�ɕϊ�
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
/// �t�F�[�h�̃A�j���[�V����
/// </summary>
public enum FadeAnimationType
{
    Sinple,
    Wave,
}

/// <summary>
/// �摜�̃t�F�[�h�C���A�E�g
/// </summary>
public enum FadeType
{
    In,
    Out,
}
