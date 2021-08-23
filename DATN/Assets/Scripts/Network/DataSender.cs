using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pbdson;

public class DataSender
{
    #region A_SERVICE_ID
    public const int CACULATOR = 100;

    #endregion

    //
    private NetworkController m_NetworkController = NetworkController.GetInstance();

    public DataSender()
    {

    }

    public void SendDataCaculator(String url)
    {
        m_NetworkController.SendHttpRequest(CACULATOR, url);
    }
}


