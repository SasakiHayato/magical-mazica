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

        public void Setup()
        {

        }

        /// <summary>
        /// �v���C���[�̗̑̓X���C�_�[���X�V����
        /// </summary>
        /// <param name="life"></param>
        public void UpdateLifeSlider(int life)
        {
            _slider.value = life;
        }
    }
}
