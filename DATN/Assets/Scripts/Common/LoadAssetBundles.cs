using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadAssetBundles : MonoBehaviour
{
    public string BundleURL;
    public Image img;
    private string AssetName = "done_ico";

    //IEnumerator Start()
    //{
    //    // Download the file from the URL. It will not be saved in the Cache
    //    using (WWW www = new WWW(BundleURL))
    //    {
    //        yield return www;
    //        if (www.error != null)
    //            throw new Exception("WWW download had an error:" + www.error);
    //        AssetBundle bundle = www.assetBundle;
    //        if (AssetName == "")
    //            Instantiate(bundle.mainAsset);
    //        else
    //        {
    //            Sprite dot = bundle.LoadAsset<Sprite>(AssetName);
    //            img.sprite = dot;
    //        }
    //        if (System.IO.File.Exists(Application.dataPath + "/Languages/English.unity3d")) Debug.Log("File Exists");

    //        // Unload the AssetBundles compressed contents to conserve memory
    //        bundle.Unload(false);

    //    }
    //    StartCoroutine(GetText());
    //}
    private void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        Debug.LogError("load ");
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(BundleURL))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            }
        }
    }

}
