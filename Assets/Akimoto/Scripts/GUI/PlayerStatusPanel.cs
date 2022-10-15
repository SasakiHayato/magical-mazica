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
    public class PlayerStatusPanel : MonoBehaviour
    {
        [SerializeField] Slider _slider;

        public void UpdateLifeSlider(int life)
        {
            _slider.value = life;
        }

        public void Setup()
        {

        }
    }
}
