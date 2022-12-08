using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace UIManagement
{
    /// <summary>
    /// 持ってる素材の所持数や選択状態を表示する
    /// </summary>
    [System.Serializable]
    public class MaterialViewPanel : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Text _text;
        [SerializeField] Image _activeImage;
        /// <summary>選択中の画像の色</summary>
        [SerializeField] Color _activeColor = Color.white;
        /// <summary>被活性状態の画像の色</summary>
        [SerializeField] Color _disableColor = Color.white;
        /// <summary>素材不足時のテキストの色</summary>
        [SerializeField] Color _shortageTextColor = Color.white;
        private MaterialPanelState _state;
        /// <summary>残り個数の表示</summary>
        public int SetNumText
        {
            set
            {
                if (CurrentMaterialID == RawMaterialID.Empty)
                {
                    _text.text = "";
                    return;
                }

                if (value <= 0)
                {
                    _text.color = _shortageTextColor;
                    _state = MaterialPanelState.Disable;
                }
                else
                {
                    _text.color = Color.black;
                    _state = MaterialPanelState.Neutral;
                }
                _text.text = value.ToString();
            }
        }
        /// <summary>状態</summary>
        public MaterialPanelState State
        {
            get { return _state; }
            set
            {
                if (CurrentMaterialID == RawMaterialID.Empty)
                {
                    _activeImage.color = _disableColor;
                    return;
                }

                _state = value;
                switch (_state)
                {
                    case MaterialPanelState.Neutral:
                        ShortageJudge(Color.clear);
                        break;
                    case MaterialPanelState.Active:
                        ShortageJudge(_activeColor);
                        break;
                    case MaterialPanelState.Disable:
                        _activeImage.color = _disableColor;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 現在表示中の素材のID
        /// </summary>
        public RawMaterialID CurrentMaterialID
        {
            get;
            private set;
        }

        /// <summary>表示する画像の設定</summary>
        /// <param name="data">表示する素材ID</param>
        public void SetData(RawMaterialDatabase data)
        {
            _image.sprite = data.Sprite;
            _image.color = data.SpriteColor;
            CurrentMaterialID = data.ID;
        }

        /// <summary>
        /// 素材不足分の判定
        /// </summary>
        private void ShortageJudge(Color elseColor)
        {
            if (int.Parse(_text.text) <= 0)
            {
                _activeImage.color = _disableColor;
            }
            else
            {
                _activeImage.color = elseColor;
            }
        }
    }

    public enum MaterialPanelState
    {
        Neutral,
        Active,
        Disable,
    }
}
