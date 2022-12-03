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
    /// �����Ă�f�ނ̏�������I����Ԃ�\������
    /// </summary>
    [System.Serializable]
    public class MaterialViewPanel : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Text _text;
        [SerializeField] Image _activeImage;
        [SerializeField] Color _activeColor = Color.white;
        [SerializeField] Color _disableColor = Color.white;
        [SerializeField] Color _shortageTextColor = Color.white;
        private MaterialPanelState _state;
        /// <summary>�f�މ摜�̕\��</summary>
        public Sprite SetMaterialSprite { set => _image.sprite = value; }
        /// <summary>�c����̕\��</summary>
        public int SetNumText
        {
            set
            {
                if (value <= 0)
                {
                    _text.color = _shortageTextColor;
                }
                _text.text = value.ToString();
            }
        }
        /// <summary>���</summary>
        public MaterialPanelState MaterialPanelState
        {
            get { return _state; }
            set
            {
                _state = value;
                switch (_state)
                {
                    case MaterialPanelState.Neutral:
                        _activeImage.color = Color.clear;
                        break;
                    case MaterialPanelState.Active:
                        _activeImage.color = _activeColor;
                        break;
                    case MaterialPanelState.Disable:
                        _activeImage.color = _disableColor;
                        break;
                    default:
                        break;
                }
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
