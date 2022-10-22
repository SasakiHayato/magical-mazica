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

        public void SetSlider(Player player)
        {
            //Sliderの最大値設定
            _slider.maxValue = player.MaxHP;
            _slider.value = player.MaxHP;

            //Playerの現在HPの監視
            player.CurrentHP
                .Subscribe(x => _slider.value = x)
                .AddTo(player);
        }
    }
}
