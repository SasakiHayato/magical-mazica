using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

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
        [SerializeField] List<MaterialViewPanel> _materialViewPanels;

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

        //memo
        //�f�ޔz��̊e�v�f���ǂ̃{�^���ɑΉ����Ă��邩�����Ă���

        public void SetMaterialSprite()
        {
        }
    }
}
