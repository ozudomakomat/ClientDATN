using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pbdson;

public class DataSender
{
    #region A_SERVICE_ID
    //
   /*
                 

    */

    //Service http
    public const int LOGIN_ACCOUNT = 7891;
    public const int LOGIN_QUICK = 7792;
    public const int LOGIN_FB = 7793;
    public const int LINK_ACCOUNT = 7794;
    public const int LOGIN_GOSU = 7795;
    public const int LOGIN_GOSU_BY_ID = 7796;
    //
    public const int REGISTER = 7892;
    public const int CHANGE_PASS = 7893;
    public const int REQUEST_LIST_ASSEST_BUNLDE = 7894;
    public const int SERVICE_ERRROR_NETWORK = 7895;
    public const int GET_INFO_MONTH_CARD = 7896;
    // 
    public const int GET_BANG_XEP_HANG = 7897;
    public const int GET_LIST_PLAYER_RATINGS = 7898;
    //
    public const int REQUEST_LIST_ASSEST_BUNLDE_TO_DEBUG = 7899;
    #endregion

    //
    private NetworkController m_NetworkController = NetworkController.GetInstance();

    public DataSender()
    {

    }


    #region COMMON
  

    public void LoginByAccount(string usn, string pwd)
    {
        List<HttpParam> lstParam = new List<HttpParam>();
        //
        string salt = System.DateTime.Now.Millisecond + "";
        string md5sum = Utils.Md5Sum(Utils.Md5Sum(usn + pwd + salt));

        //sstring url = URL_LOGIN + "login?username=" + WWW.EscapeURL (username) + "
        //&password=" + WWW.EscapeURL (password) + 
        //"&salt=" + salt + "&checksum=" + md5sum;
        //&os=WEB&session=false&cp=web
        //
        lstParam.Add(new HttpParam("username", WWW.EscapeURL(usn)));
        lstParam.Add(new HttpParam("password", WWW.EscapeURL(pwd)));
        lstParam.Add(new HttpParam("salt", salt));
        lstParam.Add(new HttpParam("checksum", md5sum));
        lstParam.Add(new HttpParam("session", "true"));
        //

        //Common params
        lstParam.Add(new HttpParam("os", Constants.OS));
       // lstParam.Add(new HttpParam("cp", Utils.GetCp()));
        string m_Udid = "test";
        lstParam.Add(new HttpParam("udid", WWW.EscapeURL(m_Udid)));
        lstParam.Add(new HttpParam("os", WWW.EscapeURL(Constants.OS)));
        lstParam.Add(new HttpParam("device", WWW.EscapeURL(SystemInfo.deviceModel)));
        lstParam.Add(new HttpParam("version", WWW.EscapeURL(Constants.VERSION)));
        lstParam.Add(new HttpParam("operator", WWW.EscapeURL(Utils.GetNetworkOperatorName())));


        if (m_NetworkController == null)
        {
            m_NetworkController = NetworkController.GetInstance();
        }
        //
       // m_NetworkController.SendHttpRequest(LOGIN_ACCOUNT, URL_SERVER_LOGIN + "login", lstParam);
    }

    #endregion
}


