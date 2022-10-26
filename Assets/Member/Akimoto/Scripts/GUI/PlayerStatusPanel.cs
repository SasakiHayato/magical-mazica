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
        [SerializeField] Text _text;
        [SerializeField] List<Image> _materialImages;

        public void SetSlider(Player player)
        {
            //Sliderの初期設定
            _slider.maxValue = player.MaxHP;
            _slider.value = player.MaxHP;
            //Textの設定
            _text.text = $"{player.MaxHP} / {player.MaxHP}";

            //Playerの現在HPに合わせてスライダーとテキストを更新する
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
