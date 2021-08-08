using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static Global instance;

    public static Global Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Global();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(gameObject);
            DbManager.GetInstance().LoadAllDb();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public int IdItemActive
    {
        get
        {
            return PlayerPrefs.GetInt(Constant.KEY_ITEM_ACTIVE, -1);
        }
    }

    public void SetItemActive(int idItem)
    {
        PlayerPrefs.SetInt(Constant.KEY_ITEM_ACTIVE, idItem);
    }

    public bool CheckItemBuy(int idItem)
    {
        String arrItem = PlayerPrefs.GetString(Constant.KEY_ITEM_IAP);
        if (string.IsNullOrEmpty(arrItem))
        {
            List<int> data = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                data.Add(i);
                data.Add(0);
            }
            arrItem = Utils.ConvertListToString(data);
            PlayerPrefs.SetString(Constant.KEY_ITEM_IAP, arrItem);
        }
        List<int> lstData = Utils.ConvertStringToListInt(arrItem);
        for (int i = 0; i < lstData.Count; i += 2)
        {
            if (lstData[i] == idItem && lstData[i + 1] != 0)
                return true;
        }
        return false;
    }
    public bool BuyItemId(int idItem)
    {
        String arrItem = PlayerPrefs.GetString(Constant.KEY_ITEM_IAP);
        if (string.IsNullOrEmpty(arrItem))
        {
            List<int> data = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                data.Add(i);
                data.Add(0);
            }
            arrItem = Utils.ConvertListToString(data);
            PlayerPrefs.SetString(Constant.KEY_ITEM_IAP, arrItem);
        }
        List<int> lstData = Utils.ConvertStringToListInt(arrItem);
        for (int i = 0; i < lstData.Count; i += 2)
        {
            if (lstData[i] == idItem)
            {
                lstData[i + 1] = 1;
                PlayerPrefs.SetString(Constant.KEY_ITEM_IAP, Utils.ConvertListToString(lstData));
                return true;
            }

        }
        return false;
    }
    public static int AddSpeed()
    {
        int speed = 0;
        String arrItem = PlayerPrefs.GetString(Constant.KEY_ITEM_IAP);
        if (string.IsNullOrEmpty(arrItem))
        {
            List<int> data = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                data.Add(i);
                data.Add(0);
            }
            arrItem = Utils.ConvertListToString(data);
            PlayerPrefs.SetString(Constant.KEY_ITEM_IAP, arrItem);
        }
        List<int> lstData = Utils.ConvertStringToListInt(arrItem);
        for (int i = 1; i < lstData.Count; i += 2)
        {
            if (lstData[i] == 1)
            {
                speed++;
            }

        }
        return speed;
    }
}
