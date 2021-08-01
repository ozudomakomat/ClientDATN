using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpParam
{
    public string m_Param = "";
    public string m_Value = "";
    //

    public HttpParam(string param, string value)
    {
        this.m_Param = param;
        this.m_Value = value;
    }

    public string GetParam()
    {
        string tmp = m_Value.Replace(" ", "%20");
        return this.m_Param + "=" + this.m_Value;
    }
}

public class HttpRequestData
{
    public string m_UrlRequest = "";
    public int m_ServiceId = -1;

    public HttpRequestData(int serviceId, string urlRequest)
    {
        m_ServiceId = serviceId;
        m_UrlRequest = urlRequest;
    }
}
