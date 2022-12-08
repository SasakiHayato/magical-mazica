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
    /// <summary>
    /// PositionのGameObjectを非表示にする
    /// </summary>
    /// <param name="hideNum">配列番号</param>
    public void HideItemPos(int hideNum)
    {
        _itemCreatePos[hideNum].SetActive(false);
    }
    /// <summary>
    /// 渡された番号のItemPosを返す
    /// </summary>
    /// <param name="posNum">配列番号</param>
    /// <returns>配列のTransform</returns>
    public Transform CreateItemPos(int posNum)
    {
        if (ItemCreatePos[posNum] != null)//なかったらランダムな場所を返す
        {
            int rnd = new System.Random().Next(0, _itemCreatePos.Length);
            return ItemCreatePos[rnd].transform;
        }
        return ItemCreatePos[posNum].transform;
    }
}
