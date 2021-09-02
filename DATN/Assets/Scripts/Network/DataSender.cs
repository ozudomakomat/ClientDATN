using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pbdson;

public class DataSender
{
    #region A_SERVICE_ID
    public const int CACULATOR = 100;
    public const int NOISUY1C = 101;
    public const int NOISUY2C = 102;
    public const int HISTORY = 103;

    #endregion

    //
    private NetworkController m_NetworkController = NetworkController.GetInstance();

    public DataSender()
    {

    }

    public void SendDataCaculator(int idGroup, int idBeTong, int idThep, float n, float mx, float my, int a, int cx, int cy, int l)
    {
        string url = DataCaculator.URL_BASE + "/api/caculator/{muid}_{idGroup}_{idBeTong}_{idThep}_{n}_{mx}_{my}_{a}_{cx}_{cy}_{l}";
        //
        url = url.Replace("{muid}", "" + DataCaculator.m_Udid);
        url = url.Replace("{idGroup}", "" + idGroup);
        url = url.Replace("{idBeTong}", "" + idBeTong);
        url = url.Replace("{idThep}", "" + idThep);
        url = url.Replace("{n}", "" + n);
        url = url.Replace("{mx}", "" + mx);
        url = url.Replace("{my}", "" + my);
        url = url.Replace("{a}", "" + a);
        url = url.Replace("{cx}", "" + cx);
        url = url.Replace("{cy}", "" + cy);
        url = url.Replace("{l}", "" + l);
        m_NetworkController.SendHttpRequest(CACULATOR, url);
    }
    public void SendDataNoiSuy1C(float x, float x1, float x2, float y1, float y2)
    {
        string url = DataCaculator.URL_BASE + "/api/noisuy1c/{x}_{x1}_{x2}_{y1}_{y2}";
        url = url.Replace("{x}", "" + x);
        url = url.Replace("{x1}", "" + x1);
        url = url.Replace("{x2}", "" + x2);
        url = url.Replace("{y1}", "" + y1);
        url = url.Replace("{y2}", "" + y2);
        m_NetworkController.SendHttpRequest(NOISUY1C, url);
    }

    public void SendDataGetHistory(int groupId)
    {
        string url = DataCaculator.URL_BASE + "/api/history/{muid}_{idGroup}";
        url = url.Replace("{muid}", "" + DataCaculator.m_Udid);
        url = url.Replace("{idGroup}", "" + groupId);
        m_NetworkController.SendHttpRequest(HISTORY, url);
    }
    public void SendDataNoiSuy2C(float x, float x1, float x2, float y, float y1, float y2, float q11, float q12, float q21, float q22)
    {
        string url = DataCaculator.URL_BASE + "/api/noisuy2c/{x}_{x1}_{x2}_{y}_{y1}_{y2}_{q11}_{q12}_{q21}_{q22}";
        url = url.Replace("{x}", "" + x);
        url = url.Replace("{x1}", "" + x1);
        url = url.Replace("{x2}", "" + x2);
        url = url.Replace("{y}", "" + y);
        url = url.Replace("{y1}", "" + y1);
        url = url.Replace("{y2}", "" + y2);
        url = url.Replace("{q11}", "" + q11);
        url = url.Replace("{q12}", "" + q12);
        url = url.Replace("{q21}", "" + q21);
        url = url.Replace("{q22}", "" + q22);
        m_NetworkController.SendHttpRequest(NOISUY2C, url);
    }
}


