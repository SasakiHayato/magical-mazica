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
        [SerializeField] List<Image> _materialImages;

        public void SetSlider(Player player)
        {
            //Slider�̍ő�l�ݒ�
            _slider.maxValue = player.MaxHP;
            _slider.value = player.MaxHP;

            //Player�̌���HP�̊Ď�
            player.CurrentHP
                .Subscribe(x => _slider.value = x)
                .AddTo(player);
        }
    }
}
