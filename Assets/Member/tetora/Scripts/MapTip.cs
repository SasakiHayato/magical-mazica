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
}
