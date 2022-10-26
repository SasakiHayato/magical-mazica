using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// UI�Ǘ��N���X
/// </summary>
namespace UIManagement
{
    /// <summary>
    /// �v���C���[�̏���\������
    /// </summary>
    [System.Serializable]
    public class PlayerStatusPanel
    {
        [SerializeField] Slider _slider;
        [SerializeField] Text _text;
        [SerializeField] List<Image> _materialImages;

        public void SetSlider(Player player)
        {
            //Slider�̏����ݒ�
            _slider.maxValue = player.MaxHP;
            _slider.value = player.MaxHP;
            //Text�̐ݒ�
            _text.text = $"{player.MaxHP} / {player.MaxHP}";

            //Player�̌���HP�ɍ��킹�ăX���C�_�[�ƃe�L�X�g���X�V����
            player.CurrentHP
                .Subscribe(x =>
                {
                    _slider.value = x;
                    _text.text = $"{x} / {player.MaxHP}";
                })
                .AddTo(player);
        }
    }
}
