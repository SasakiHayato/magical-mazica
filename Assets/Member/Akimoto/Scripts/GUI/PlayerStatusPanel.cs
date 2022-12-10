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
    /// プレイヤーの情報を表示する
    /// </summary>
    [System.Serializable]
    public class PlayerStatusPanel
    {
        [SerializeField] Slider _slider;
        [SerializeField] Text _text;
        [SerializeField] List<MaterialViewPanel> _materialViewPanels;

        /// <summary>
        /// スライダーの設定
        /// </summary>
        /// <param name="player">現在生成中のプレイヤー</param>
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

        /// <summary>
        /// 素材表示画面の設定
        /// </summary>
        /// <param name="player"></param>
        public void SetMaterialViewPanel(Player player)
        {
            player.SelectMaterial.Subscribe(collection =>
            {
                //選択中イベントの受け取り
                _materialViewPanels.ForEach(panel =>
                {
                    //選択された素材に含まれているものをアクティブに、されていないものをニュートラル状態にする
                    collection.ForEach(id =>
                    {
                        if (panel.CurrentMaterialID == id)
                        {
                            panel.State = MaterialPanelState.Active;
                        }
                    });

                    if (panel.State != MaterialPanelState.Active)
                    {
                        panel.State = MaterialPanelState.Neutral;
                    }
                });
            })
            .AddTo(player);

            //素材数の更新
            player.Storage.MaterialDictionary.Subscribe(dic =>
            {
                _materialViewPanels.ForEach(p =>
                {
                    if (dic.Key == p.CurrentMaterialID)
                    {
                        p.SetNumText = dic.NewValue;
                    }
                });
            })
            .AddTo(player);
        }

        /// <summary>
        /// 素材画像の設定
        /// </summary>
        /// <param name="materialSprites">表示する素材のList</param>
        public void SetMaterialSprite(List<RawMaterialDatabase> materialDatas)
        {
            for (int i = 0; i < _materialViewPanels.Count; i++)
            {
                _materialViewPanels[i].SetData(materialDatas[i]);
            }
        }
    }
}
