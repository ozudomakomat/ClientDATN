using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using pbdson;
using SimpleJSON;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{

    #region FIELDS

    private static NetworkController m_Instance;

    public static NetworkController GetInstance()
    {
        if (m_Instance == null)
        {
            //Debug.LogError ("----- Null network controller");
        }
        return m_Instance;
    }

    private List<INetEventHandler> m_LstEventHandler = new List<INetEventHandler>();

    //queued lai & gui trong update vi khi co nhieu action gui di 1 luc
    //thi bi ignore cac action sau
    private List<HttpRequestData> m_LstQueuedHttpRequest = new List<HttpRequestData>();

    //service queued lai de gui den game server
    private List<PbAction> m_LstQueuedSendAction = new List<PbAction>();

    //dung chung cho ca game server va account server
    private List<PbAction> m_LstQueuedReceiveAction = new List<PbAction>();
    //
    private readonly object syncLockSend = new object();
    private readonly object syncLockReceive = new object();

    //
    [HideInInspector]
    public string m_UrlGameServer = "http://115.84.178.84:6660";

    //
    public string MSG_ERRROR_SERVICE = "";

    #endregion

    void Awake()
    {
        Debug.Log("-------- Awake network controller");
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    //add 1 listener cho su kien cua server
    public void AddNetworkEventHandler(INetEventHandler hdl)
    {
        lock (syncLockReceive)
        {
            if (!this.m_LstEventHandler.Contains(hdl))
            {
                this.m_LstEventHandler.Add(hdl);
            }
        }
    }

    //remove 1 listener
    public void RemoveNetworkEventHandler(INetEventHandler hdl)
    {
        lock (syncLockReceive)
        {
            if (this.m_LstEventHandler.Contains(hdl))
            {
                this.m_LstEventHandler.Remove(hdl);
            }
            else
            {
              //  Debug.LogError("----- ERROR: NetEventHandler not in list");
            }
        }
    }


    public void SendHttpRequest(int serviceId, string urlServer, List<HttpParam> lstParams = null)
    {
        string strParams = "";
        //
        List<string> lstParamKey = new List<string>();
        if (lstParams != null)
        {
            int count = 0;
            foreach (HttpParam param in lstParams)
            {
                //tranh gan 2 param jong nhau
                if (lstParamKey.Contains(param.m_Param))
                {
                    continue;
                }
                lstParamKey.Add(param.m_Param);
                //
                if (count != 0)
                    strParams += "&" + param.GetParam();
                else
                    strParams += param.GetParam();
                count++;
            }
        }
        //
        lock (syncLockSend)
        {
            string urlRequest = urlServer;
            if (!Utils.IsStringEmpty(strParams))
                urlRequest = urlRequest + "?" + strParams;
            //
            m_LstQueuedHttpRequest.Add(new HttpRequestData(serviceId, urlRequest));
        }
    }

    public void SendProtoPackage(int serviceId, object protoObject = null)
    {
        lock (syncLockSend)
        {
            PbAction pbAction = new PbAction();
            //
            pbAction.actionId = serviceId;
            if (protoObject != null)
            {
                pbAction.data = Selialize(protoObject);
            }
            //
            m_LstQueuedSendAction.Add(pbAction);
        }
    }

    private PbAction GenAction(int serviceId, object data = null)
    {
        PbAction action = new PbAction();
        action.actionId = serviceId;
        if (data != null)
        {
            action.data = Selialize(data);
        }

        return action;
    }

    private byte[] Selialize(object data)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream();

        PbMethodSerializer method = new PbMethodSerializer();
        method.Serialize(ms, data);
        byte[] tmp = ms.ToArray();

        return tmp;
    }

    public static T ParsePb<T>(byte[] data)
    {
        if (data == null)
        {
            return default(T);
        }
        System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
        PbMethodSerializer method = new PbMethodSerializer();
        try
        {
            T tmp = (T)method.Deserialize(ms, null, typeof(T));
            return tmp;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
            return default(T);
        }

    }

    public static object ParsePb(string typeName, byte[] data)
    {
        if (data == null)
        {
            return null;
        }
        System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
        PbMethodSerializer method = new PbMethodSerializer();
        try
        {
            System.Type typeToParse = System.Type.GetType(typeName);
            object tmp = method.Deserialize(ms, null, typeToParse);
            return tmp;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
            return null;
        }
    }

    #region SEND PROTO PACKAGE

    private void UpdateSendListPackage()
    {
        lock (syncLockSend)
        {
            if (m_LstQueuedSendAction.Count > 0)
            {
                RequestData request = new RequestData();
                request.session = "";
                //Debug.Log ("------ session: " + GameContext.GetIntance ().m_Session);
                foreach (PbAction pbAction in m_LstQueuedSendAction)
                {
                    request.actions.Add(pbAction);
                }
                //
                SendPackage(request);
                //
                m_LstQueuedSendAction.Clear();
            }
        }
    }

    private void SendPackage(RequestData request)
    {
        //check 1 so request ko bat buoc phai wait response
        List<int> lstServiceIdNoneAsync = new List<int>() { 114, 116 };
        //xem co action nao trong request co sv nay ko
        bool needShowLoading = true;
        foreach (PbAction pbAction in request.actions)
        {
            if (lstServiceIdNoneAsync.Contains(pbAction.actionId))
            {
                needShowLoading = false;
                Debug.LogError("--------- IGNORE LOADING for sv " + pbAction.actionId);
                break;
            }
        }
        //
        if (needShowLoading)
        {
            //gc.m_CurrentScene.ShowLoading();
        }

        bool makeTimeOut = false;
        string urlGameServer;
        //
        if (makeTimeOut)
        {
            Debug.LogError("---- TEST TIME OUT WITH THIS REQUEST ----");
            urlGameServer = "http://example.com:81";
        }
        else
        {
            urlGameServer = m_UrlGameServer;
        }
        //Debug.Log ("Data length "+data.Length);
        ///StartCoroutine(CheckTimeoutWWW(www));
        ///
        string strSevicesLog = "";
        // Debug
        if (request.actions != null)
        {
            string s = "▲ Request action = ";
            for (int i = 0; i < request.actions.Count; i++)
            {
                s += request.actions[i].actionId + ", ";
                //
                if (i == 0)
                {
                    strSevicesLog = request.actions[i].actionId + "";
                }
                else
                {
                    strSevicesLog += "_" + request.actions[i].actionId;
                }
            }
            Debug.Log(s.TrimEnd(',', ' '));
        }

       // string userID = GameContext.GetIntance().m_UserId + "";
        //them thong tin de log
        urlGameServer = string.Format("{0}/i/{1}/{2}", urlGameServer,
            UnityWebRequest.EscapeURL(strSevicesLog), "");
        //
        Debug.Log("----------------- SEND REQUEST:" + urlGameServer);
        StartCoroutine(WaitForRequest(urlGameServer, request));
        //


    }

    #endregion


    #region SEND PROTO PACKAGE

    private void UpdateSendListHttpRequest()
    {
        lock (syncLockSend)
        {
            if (m_LstQueuedHttpRequest.Count > 0)
            {
                foreach (HttpRequestData request in m_LstQueuedHttpRequest)
                {
                    SendHttpRequest(request);
                }
                //
                m_LstQueuedHttpRequest.Clear();
            }
        }
    }

    private void SendHttpRequest(HttpRequestData request)
    {
        string urlRequest = request.m_UrlRequest;
        StartCoroutine(WaitForHttpRequest(request.m_ServiceId, urlRequest));
        // Debug
        string s = "▲ Request action = " + request.m_ServiceId;
        Debug.Log(s);
        Debug.Log(request.m_UrlRequest);
    }

    #endregion

    private IEnumerator WaitForRequest(string url, RequestData requestData)
    {
        byte[] bytesDataRequest = Selialize(requestData);
        //
        string strLstSentService = "";
        int countSv = 0;
        if (requestData != null)
        {
            foreach (PbAction pbAction in requestData.actions)
            {
                if (countSv == 0)
                {
                    strLstSentService += pbAction.actionId + "";
                }
                else
                {
                    strLstSentService += ", " + pbAction.actionId;
                }
                countSv++;
            }
        }
        //
        //using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, bytesDataRequest))
        {
            webRequest.timeout = 10;
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            //no error
            if (!webRequest.isNetworkError)
            {
                //Debug.Log ("------- RUN HERE 111");
                lock (syncLockReceive)
                {
                    byte[] bytesDataReturn = webRequest.downloadHandler.data;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bytesDataReturn);

                    PbMethodSerializer method = new PbMethodSerializer();
                    ResponseData response = (ResponseData)method.Deserialize(ms, null, typeof(ResponseData));
                    //Debug.Log ("------- RUN HERE 333");
                    if (response.aAction.Count == 0)
                    {
                        Debug.LogError("---- Received no PbAction");
                        Toast.ShowToast(string.Format(MSG_ERRROR_SERVICE, strLstSentService));
                    }
                    else
                    {
                        Debug.Log("---------- Count service return: " + response.aAction.Count);
                        foreach (PbAction pbAction in response.aAction)
                        {
                            Debug.Log("▼ Response action= " + pbAction.actionId);
                            m_LstQueuedReceiveAction.Add(pbAction);
                        }
                    }
                }
            }
            else
            {
                //Debug.Log ("------- RUN HERE 222");
                Toast.ShowToast(string.Format(MSG_ERRROR_SERVICE, strLstSentService));
                Debug.LogError("--- Error: " + webRequest.error);
            }
        }
    }

    private IEnumerator WaitForHttpRequest(int serviceId, string urlRequest)
    {
        //no error
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlRequest))
        {
            webRequest.timeout = (int)10;
            yield return webRequest.SendWebRequest();
            if (!webRequest.isNetworkError)
            {
                lock (syncLockReceive)
                {
                    PbAction pbAction = new PbAction();
                    pbAction.actionId = serviceId;
                    pbAction.data = webRequest.downloadHandler.data;
                    //
                    Debug.Log("▼ Response action= " + serviceId);
                    m_LstQueuedReceiveAction.Add(pbAction);
                }
            }
            else
            {
                Toast.ShowToast(string.Format(MSG_ERRROR_SERVICE, serviceId));
                Debug.LogError("--- Error: " + webRequest.error);
            }
        }
    }

    private List<INetEventHandler> m_LstTempEventHandler = new List<INetEventHandler>();

    private void UpdateProcessReceivedPackage()
    {
        lock (syncLockReceive)
        {
            if (m_LstQueuedReceiveAction.Count > 0)
            {
                foreach (PbAction action in m_LstQueuedReceiveAction)
                {
                    PreProcessKEvent(action.actionId, action.data);
                    m_LstTempEventHandler.Clear();
                    m_LstTempEventHandler.AddRange(m_LstEventHandler);
                    foreach (INetEventHandler eventHandler in m_LstTempEventHandler)
                    {
                        eventHandler.ProcessKEvent(action.actionId, action.data);
                    }
                }
                //
                m_LstQueuedReceiveAction.Clear();
            }
        }
    }

    private void PreProcessKEvent(int eventId, byte[] data)
    {
        //DbManager dbMng = DbManager.GetInstance ();
        //
        switch (eventId)
        {
            
        }
    }

    public void ProcessPackageEvent(int action, byte[] byte_data)
    {
        string data = System.Text.Encoding.UTF8.GetString((byte[])byte_data);
        //Debug.Log ("stringBonus>>>" + data);
        if (!string.IsNullOrEmpty(data))
        {
            JSONNode jNode = JSONNode.Parse(data);
            if (jNode == null)
            {
                return;
            }
            //
            JSONClass root = jNode.AsObject;
            int successStatus = root["status"].AsInt;
            if (successStatus == 0)
            {
                
            }
            else
            {
                string msg = root["desc"];
                Toast.ShowToast(msg);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //gui http request den acc server
        UpdateSendListHttpRequest();

        //gui http request den game server
        UpdateSendListPackage();

        //
        UpdateProcessReceivedPackage();
    }
}