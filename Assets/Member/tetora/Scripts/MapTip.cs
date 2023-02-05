using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTip : MonoBehaviour
{
    [SerializeField]
    GameObject[] _itemCreatePos;
    [SerializeField, Tooltip("アイテムを降らせる場所の数")]
    int _itemCount = 4;

    public GameObject[] ItemCreatePos { get => _itemCreatePos; }

    private void Start()
    {
        SetItemPos();
    }
    /// <summary>
    /// 最初に使うPositionのGameObjectを表示する
    /// </summary>
    void SetItemPos()
    {
        for (int i = 0; i < _itemCount; i++)
        {
            ItemCreatePos[i].SetActive(true);//指定した数だけアイテムを降らせる場所を表示する
        }
    }
}
