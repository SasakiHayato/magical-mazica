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
        /// <summary>�I�𒆂̉摜�̐F</summary>
        [SerializeField] Color _activeColor = Color.white;
        /// <summary>�튈����Ԃ̉摜�̐F</summary>
        [SerializeField] Color _disableColor = Color.white;
        /// <summary>�f�ޕs�����̃e�L�X�g�̐F</summary>
        [SerializeField] Color _shortageTextColor = Color.white;
        private MaterialPanelState _state;
        /// <summary>�c����̕\��</summary>
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
        /// <summary>���</summary>
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
        /// ���ݕ\�����̑f�ނ�ID
        /// </summary>
        public RawMaterialID CurrentMaterialID
        {
            get;
            private set;
        }

        /// <summary>�\������摜�̐ݒ�</summary>
        /// <param name="data">�\������f��ID</param>
        public void SetData(RawMaterialDatabase data)
        {
            _image.sprite = data.Sprite;
            _image.color = data.SpriteColor;
            CurrentMaterialID = data.ID;
        }

        /// <summary>
        /// �f�ޕs�����̔���
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
