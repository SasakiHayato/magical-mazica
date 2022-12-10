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
    /// �v���C���[�̏���\������
    /// </summary>
    [System.Serializable]
    public class PlayerStatusPanel
    {
        [SerializeField] Slider _slider;
        [SerializeField] Text _text;
        [SerializeField] List<MaterialViewPanel> _materialViewPanels;

        /// <summary>
        /// �X���C�_�[�̐ݒ�
        /// </summary>
        /// <param name="player">���ݐ������̃v���C���[</param>
        public void SetSlider(Player player)
        {
            //Slider�̏����ݒ�
            _slider.maxValue = player.MaxHP;
            _slider.value = player.MaxHP;
            //Text�̐ݒ�
            _text.text = $"{player.MaxHP} / {player.MaxHP}";

            //Player�̌���HP�ɍ��킹�ăX���C�_�[�ƃe�L�X�g���X�V����
            player.CurrentHP
                .Subscribe(x =>
                {
                    _slider.value = x;
                    _text.text = $"{x} / {player.MaxHP}";
                })
                .AddTo(player);
        }

        /// <summary>
        /// �f�ޕ\����ʂ̐ݒ�
        /// </summary>
        /// <param name="player"></param>
        public void SetMaterialViewPanel(Player player)
        {
            player.SelectMaterial.Subscribe(collection =>
            {
                //�I�𒆃C�x���g�̎󂯎��
                _materialViewPanels.ForEach(panel =>
                {
                    //�I�����ꂽ�f�ނɊ܂܂�Ă�����̂��A�N�e�B�u�ɁA����Ă��Ȃ����̂��j���[�g������Ԃɂ���
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

            //�f�ސ��̍X�V
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
        /// �f�މ摜�̐ݒ�
        /// </summary>
        /// <param name="materialSprites">�\������f�ނ�List</param>
        public void SetMaterialSprite(List<RawMaterialDatabase> materialDatas)
        {
            for (int i = 0; i < _materialViewPanels.Count; i++)
            {
                _materialViewPanels[i].SetData(materialDatas[i]);
            }
        }
    }
}
