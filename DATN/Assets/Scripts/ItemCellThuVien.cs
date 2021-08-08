using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellThuVien : MonoBehaviour
{
    [SerializeField] Text m_TextName;
    private ItemCellTVInfo data;
    public Action<int> callback;

    public void SetData(ItemCellTVInfo data)
    {
        this.data = data;
        m_TextName.text = data.m_name;
    }

    public void ButtonItemClick()
    {
        if (callback != null)
        {
            callback(data.id);
        }
    }
}

public class ItemCellTVInfo
{
    public string m_name;
    public int id;

    public ItemCellTVInfo(string name, int id)
    {
        m_name = name;
        this.id = id;
    }
}
