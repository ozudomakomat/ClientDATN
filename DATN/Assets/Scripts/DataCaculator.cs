using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCaculator
{
    private static DataCaculator instance;
    public static string URL_BASE = "http://192.168.204.128:8080";
    public DataSender dataSender = new DataSender();
    public static DataCaculator GetInstance()
    {
        if (instance == null)
        {
            instance = new DataCaculator();
        }
        if (string.IsNullOrEmpty(m_Udid))
        {
            m_Udid = SystemInfo.deviceUniqueIdentifier;
        }
        return instance;
    }
    public static CDBT cdbt;
    public static CDT cdt;
    public static DataNoiBo data;
    public int groupId;
    public int soLuongThep;
    public static BoTriThep boTriThep;

    public static float NoiSuy1C(float x1, float x2, float y1, float y2, float x)
    {
        return y2 + (y1 - y2) * (x - x2) / (x1 - x2);
    }
    public static string m_Udid = SystemInfo.deviceUniqueIdentifier;
}
