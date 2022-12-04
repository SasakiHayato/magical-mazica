using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTip : MonoBehaviour
{
    [SerializeField]
    GameObject[] _itemCreatePos;
    [SerializeField, Tooltip("�A�C�e�����~�点��ꏊ�̐�")]
    int _itemCount = 4;

    public GameObject[] ItemCreatePos { get => _itemCreatePos; }

    private void Start()
    {
        SetItemPos();
    }
    /// <summary>
    /// �ŏ��Ɏg��Position��GameObject��\������
    /// </summary>
    void SetItemPos()
    {
        for (int i = 0; i < _itemCount; i++)
        {
            ItemCreatePos[i].SetActive(true);//�w�肵���������A�C�e�����~�点��ꏊ��\������
        }
    }
    /// <summary>
    /// Position��GameObject���\���ɂ���
    /// </summary>
    /// <param name="hideNum">�z��ԍ�</param>
    public void HideItemPos(int hideNum)
    {
        _itemCreatePos[hideNum].SetActive(false);
    }
    /// <summary>
    /// �n���ꂽ�ԍ���ItemPos��Ԃ�
    /// </summary>
    /// <param name="posNum">�z��ԍ�</param>
    /// <returns>�z���Transform</returns>
    public Transform CreateItemPos(int posNum)
    {
        if (ItemCreatePos[posNum] != null)//�Ȃ������烉���_���ȏꏊ��Ԃ�
        {
            int rnd = new System.Random().Next(0, _itemCreatePos.Length);
            return ItemCreatePos[rnd].transform;
        }
        return ItemCreatePos[posNum].transform;
    }
}
