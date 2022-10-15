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
