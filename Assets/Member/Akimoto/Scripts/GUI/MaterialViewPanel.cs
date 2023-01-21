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
        /// <summary>非活性状態の画像の色</summary>
        [SerializeField] Color _disableColor = Color.white;
        /// <summary>素材不足時のテキストの色</summary>
        [SerializeField] Color _shortageTextColor = Color.white;
        /// <summary>選択中アニメーション</summary>
        [SerializeField] Image _selectedAnimImg;
        /// <summary>選択されたアニメーション</summary>
        [SerializeField] GameObject _selectAnimationObject;
        [SerializeField] float _selectedAnimationDuraion;
        private Color _defColor;
        private MaterialPanelState _state;
        private Sequence _sequence;
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

                if (_sequence != null)
                {
                    _sequence.Kill();
                }

                _state = value;
                _selectAnimationObject.SetActive(false);
                switch (_state)
                {
                    case MaterialPanelState.Neutral:
                        _selectedAnimImg.gameObject.SetActive(false);
                        _activeImage.color = Color.clear;
                        break;
                    case MaterialPanelState.Active:
                        _selectAnimationObject.SetActive(true);
                        _selectedAnimImg.gameObject.SetActive(true);
                        _sequence = DOTween.Sequence();
                        _sequence
                            .Append(_selectedAnimImg.DOColor(Color.white, _selectedAnimationDuraion))
                            .Append(_selectedAnimImg.DOColor(_defColor, _selectedAnimationDuraion))
                            .SetLoops(-1);
                        break;
                    case MaterialPanelState.Disable:
                        _selectedAnimImg.gameObject.SetActive(false);
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
            _selectedAnimImg.sprite = data.Sprite;
            _selectedAnimImg.color = data.SpriteColor;
            _defColor = data.SpriteColor;
            CurrentMaterialID = data.ID;
            _selectAnimationObject.SetActive(false);
        }

        /// <summary>
        /// 素材不足分の判定
        /// </summary>
        //private void ShortageJudge(Color elseColor)
        //{
        //    if (int.Parse(_text.text) <= 0)
        //    {
        //        _activeImage.color = _disableColor;
        //        _selectedAnimImg.gameObject.SetActive(false);
        //        if (_sequence != null)
        //        {
        //            _sequence.Kill();
        //        }
        //    }
        //    else
        //    {
        //        //_activeImage.color = elseColor;
        //        _selectedAnimImg.gameObject.SetActive(true);
        //        Color defColor = _selectedAnimImg.color;
        //        _sequence = DOTween.Sequence();
        //        _sequence
        //            .Append(_selectedAnimImg.DOColor(Color.white, _selectedAnimationDuraion))
        //            .Append(_selectedAnimImg.DOColor(defColor, _selectedAnimationDuraion))
        //            .SetLoops(-1);
        //    }
        //}
    }

    /// <summary>
    /// 素材表示画面の状態
    /// </summary>
    public enum MaterialPanelState
    {
        Neutral,
        Active,
        Disable,
    }
}
