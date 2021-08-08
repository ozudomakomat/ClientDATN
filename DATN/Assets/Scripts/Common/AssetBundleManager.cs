using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;

public class AssetBundleManager
{
    private static AssetBundleManager m_Instance;

    public static AssetBundleManager GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new AssetBundleManager();
        }
        //
        return m_Instance;
    }

    #region FIELDS


    //Db
    public const int BUNDLE_DB = 1;

    private List<AssetInfo> m_LstAssetInfo = new List<AssetInfo>();
    private Dictionary<int, AssetBundle> m_DictAssetBundle = new Dictionary<int, AssetBundle>();
    //
    public System.Action<string, float, string> m_DownloadAssetProgressCallback;
    public System.Action<bool> m_DownloadAssetDoneCallback;

    #endregion

    private AssetBundleManager()
    {

    }
    public void GenAssetDataFile()
    {
        string host = "http://127.0.0.1/";
        m_LstAssetInfo.Add(new AssetInfo(BUNDLE_DB, "Db", host + "image_base", 1));

    }
    public void SetListAssetBundleInfo(List<AssetInfo> lstAssetInfo)
    {
        m_LstAssetInfo = lstAssetInfo;
    }

    public AssetBundle GetAssetBundle(int bundleId)
    {
        if (!m_DictAssetBundle.ContainsKey(bundleId))
        {
            return null;
        }
        //
        return (AssetBundle)m_DictAssetBundle[bundleId];
    }

    public Sprite LoadSprite(int bundleId, string imageName)
    {
        if (!m_DictAssetBundle.ContainsKey(bundleId))
        {
            return null;
        }
        //
        AssetBundle bundle = (AssetBundle)m_DictAssetBundle[bundleId];
        //
        if (bundle == null)
        {
            return null;
        }
        //
        if (!bundle.Contains(imageName))
        {
            return null;
        }
        //
        return bundle.LoadAsset<Sprite>(imageName);
    }

    public string LoadText(int bundleId, string fileName)
    {
        if (!m_DictAssetBundle.ContainsKey(bundleId))
        {
            return null;
        }
        //
        AssetBundle bundle = (AssetBundle)m_DictAssetBundle[bundleId];
        if (bundle == null)
        {
            return "";
        }
        //
        if (!bundle.Contains(fileName))
        {
            return "";
        }
        //
        TextAsset textAsset = bundle.LoadAsset<TextAsset>(fileName);
        string ret = "";
        if (textAsset != null)
        {
            ret = textAsset.text;
        }
        return ret;
    }

    public UnityEngine.Object LoadUnityObject(int bundleId, string assetName)
    {
        if (!m_DictAssetBundle.ContainsKey(bundleId))
        {
            return null;
        }
        //
        AssetBundle bundle = (AssetBundle)m_DictAssetBundle[bundleId];
        if (bundle == null)
        {
            return null;
        }
        //
        if (!bundle.Contains(assetName))
        {
            return null;
        }
        //
        return bundle.LoadAsset(assetName);
    }

    AssetBundle bundle;
    public UnityEngine.Object LoadUnityObject2(int bundleId, string assetName)
    {
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(Application.dataPath + "/Resource/Data" + assetName);
        }
        return bundle.LoadAsset(assetName);
    }
    
    // Download an AssetBundle
    public IEnumerator DownloadAllAssetBundle()
    {
        GenAssetDataFile();
        yield return null;
        //
        while (!Caching.ready)
        {
            yield return null;
        }
        //
        bool hasError = false;

        try
        {
            foreach (AssetBundle assetBundle in m_DictAssetBundle.Values)
            {
                if (assetBundle != null)
                {
                    assetBundle.Unload(true);
                }
                else
                {
                    Debug.LogError("------- NULL ASSET BUNDLE CAN NOT UNLOADED");
                }
            }
        }
        catch
        {
            Debug.LogError("------- Error Unload Assets Bundles");
        }

        m_DictAssetBundle.Clear();
        if (m_LstAssetInfo == null)
        {
            m_LstAssetInfo = new List<AssetInfo>();
        }

        List<AssetInfo> lstAssetInfoNeedUpdate = new List<AssetInfo>();
        //
        foreach (AssetInfo assetInfo in m_LstAssetInfo)
        {

            Debug.Log("------- RUN HERE : " + assetInfo.m_AssetUrl);
            if (Caching.IsVersionCached(assetInfo.m_AssetUrl, assetInfo.m_Version))
            {
                Debug.Log("------ Load caching asset: " + assetInfo.m_AssetUrl);
                WWW www = WWW.LoadFromCacheOrDownload(assetInfo.m_AssetUrl, assetInfo.m_Version);
                yield return www;
                //
                if (!m_DictAssetBundle.ContainsKey(assetInfo.m_BundleId))
                {
                    m_DictAssetBundle.Add(assetInfo.m_BundleId, www.assetBundle);
                }
            }
            else
            {
                lstAssetInfoNeedUpdate.Add(assetInfo);
            }
        }

        //Download asset need update
        //
        if (lstAssetInfoNeedUpdate.Count > 0)
        {
            //PanelLoadingAsset.Instance.ShowSlider ();

            float accumulatedProgress = 0f;

            int numBundle = lstAssetInfoNeedUpdate.Count;

            for (int i = 0; i < numBundle; i++)
            {
                AssetInfo assetInfoUpdate = lstAssetInfoNeedUpdate[i];
                Debug.Log("------ Download asset url: " + assetInfoUpdate.m_AssetUrl);

                string assetDisplayName = assetInfoUpdate.m_AssetName;

                WWW www = WWW.LoadFromCacheOrDownload(assetInfoUpdate.m_AssetUrl, assetInfoUpdate.m_Version);

                while (!www.isDone)
                {
                    float assetProgress = www.progress;

                    if (m_DownloadAssetProgressCallback != null)
                    {
                        string strCountBundle = (i + 1) + "/" + numBundle;
                        m_DownloadAssetProgressCallback(assetDisplayName, assetProgress, strCountBundle);
                    }
                    yield return null;
                }

                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogError("-------- Error downloading assset: " + www.error);
                    hasError = true;
                    break;
                }
                Debug.Log(" ------ Size resources " + www.bytesDownloaded);
                if (www.bytesDownloaded == 0)
                {
                    hasError = true;
                    break;
                }

                if (!Directory.Exists(Application.dataPath + "/Resource/Data"))
                    Directory.CreateDirectory(Application.dataPath + "/Resource/Data");
                byte[] bytes = www.bytes;
                File.WriteAllBytes(Application.dataPath + "/Resource/Data" + assetInfoUpdate.m_AssetName, bytes);
                Debug.Log("da ghi ra file");
                Debug.Log(Application.dataPath + "/Resource/Data" + assetInfoUpdate.m_AssetName);




                Debug.LogError("--------- download aseset done");
                //
                string strCountBundle2 = (i + 1) + "/" + numBundle;
                //
                if (m_DownloadAssetProgressCallback != null)
                {
                    m_DownloadAssetProgressCallback(assetDisplayName, 1f, strCountBundle2);
                }

                yield return new WaitForSeconds(1f);

                if (!m_DictAssetBundle.ContainsKey(assetInfoUpdate.m_BundleId))
                {
                    m_DictAssetBundle.Add(assetInfoUpdate.m_BundleId, www.assetBundle);
                }
            }
        }


        yield return new WaitForSeconds(0.1f);
        //
        if (m_DownloadAssetDoneCallback != null)
        {
            m_DownloadAssetDoneCallback(hasError);
        }
    }
}

public class AssetInfo
{
    public int m_BundleId = 1;
    public string m_AssetName = "";
    public string m_AssetUrl = "";
    public float m_Size = 0;
    public int m_Version = 1;

    //
    public AssetInfo(int bundleId, string assetName, string assetUrl, int version)
    {
        this.m_BundleId = bundleId;
        this.m_AssetName = assetName;
        this.m_AssetUrl = assetUrl;
        this.m_Version = version;

    }

    public AssetInfo(int bundleId, string assetName, string assetUrl, float size, int version)
    {
        this.m_BundleId = bundleId;
        this.m_AssetName = assetName;
        this.m_AssetUrl = assetUrl;
        this.m_Version = version;
        this.m_Size = size;

    }
}
