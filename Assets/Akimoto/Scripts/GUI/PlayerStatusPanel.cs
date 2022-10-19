using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// UI管理クラス
/// </summary>
namespace UIManagement
{
    /// <summary>
    /// プレイヤーの情報を表示する
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
        /// プレイヤーの体力スライダーを更新する
        /// </summary>
        /// <param name="life"></param>
        public void UpdateLifeSlider(int life)
        {
            _slider.value = life;
        }
    }
}
