using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace UIManagement
{
    [System.Serializable]
    public class Popup
    {
        [SerializeField] Image _backgroundImage;
        [SerializeField] Text _mainText;
        [SerializeField] Text _positiveButtonText;
        [SerializeField] Button _positiveButton;
        [SerializeField] Text _negativeButtonText;
        [SerializeField] Button _negativeButton;
        [SerializeField] float _scaleChangeSpeed;
        /// <summary>初期スケール</summary>
        private Vector2 _defScale;
        private System.Action _positiveEvent;
        private System.Action _negativeEvent;
        /// <summary>クリック待機フラグ</summary>
        private bool _isWaitClick;
        /// <summary>選択可能グラグ</summary>
        private bool _selectable;
        /// <summary>Popupの表示非表示</summary>
        public bool SetActive { set => _backgroundImage.gameObject.SetActive(value); }

        public void Setup(Component component)
        {
            //各ボタンの設定
            _positiveButton.OnClickAsObservable()
                .Where(x => _selectable)
                .Subscribe(_ =>
                {
                    _selectable = false;
                    _positiveEvent();
                    _isWaitClick = false;
                    _selectable = true;
                })
                .AddTo(component);
            _negativeButton.OnClickAsObservable()
                .Where(x => _selectable)
                .Subscribe(_ =>
                {
                    _selectable = false;
                    _negativeEvent();
                    _isWaitClick = false;
                    _selectable = true;
                })
                .AddTo(component);

            _defScale = _backgroundImage.transform.localScale;
            _backgroundImage.rectTransform.localScale = Vector2.zero;
            Disable().Forget();
        }

        /// <summary>
        /// ポップアップの表示
        /// </summary>
        public async UniTask Active(string value, string positiveTextValue, System.Action positiveEvent, string negativeTextValue, System.Action negativeEvent)
        {
            _mainText.text = value;
            _positiveButtonText.text = positiveTextValue;
            _positiveEvent = positiveEvent;
            _negativeButtonText.text = negativeTextValue;
            _negativeEvent = negativeEvent;
            await _backgroundImage.transform.DOScale(_defScale, _scaleChangeSpeed);
            _isWaitClick = true;
            await UniTask.WaitUntil(() => !_isWaitClick); //選択されるまで待つ
            await Disable();
        }

        /// <summary>
        /// ポップアップ非表示
        /// </summary>
        /// <returns></returns>
        private async UniTask Disable()
        {
            await _backgroundImage.transform.DOScale(Vector2.zero, _scaleChangeSpeed);
        }

        public void GetSelectObservable(System.IObservable<int> observable, Component component)
        {
            observable
                .Subscribe(i => SelectButton((PopupSelectID)i))
                .AddTo(component);
        }

        public void GetSelectObservable(System.IObservable<PopupSelectID> observable, Component component)
        {
            observable
                .Subscribe(id => SelectButton(id))
                .AddTo(component);
        }

        private void SelectButton(PopupSelectID value)
        {
            switch (value)
            {
                case PopupSelectID.Negative:
                    _negativeButton.interactable = true;
                    _positiveButton.interactable = false;
                    break;
                case PopupSelectID.Positive:
                    _positiveButton.interactable = true;
                    _negativeButton.interactable = false;
                    break;
                default:
                    break;
            }
        }
    }
}

public enum PopupSelectID
{
    Negative,
    Positive,
}