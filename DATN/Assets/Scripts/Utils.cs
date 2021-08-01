using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using pbdson;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Utils
{
    public static PbAction GenNetworkErrorPbAction(int serviceId)
    {
        PbAction pbAction = new PbAction();
        pbAction.actionId = DataSender.SERVICE_ERRROR_NETWORK;
        byte[] intBytes = System.BitConverter.GetBytes(serviceId);
        //		if (System.BitConverter.IsLittleEndian)
        //			System.Array.Reverse(intBytes);
        pbAction.data = intBytes;
        //
        return pbAction;
    }

    public static float getAngle(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 targetDir = toPosition - fromPosition;


        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;

        return angle;
    }

    public static string GetNetworkOperatorName()
    {

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityPlayerActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            //
            AndroidJavaObject TM = new AndroidJavaObject("com.grepgame.lib.GrepLib");
            string carrierName = TM.CallStatic<string>("getCarrierName", unityPlayerActivity);
            if (carrierName == null)
                carrierName = "";
            return carrierName;
        }
        catch (System.Exception e)
        {
            return "";
        }
#elif UNITY_IOS
			return GgLibIOS.getOperatorName();
#else
        return "";
#endif
    }


    public static Sprite GetSpriteOfShopCurrency(ShopCurrencyType type)
    {
        Sprite sprite = null;
        if (type == ShopCurrencyType.GOLD)
        {
            sprite = Resources.Load<Sprite>("Images/Common/icon_gold");
        }
        else if (type == ShopCurrencyType.GEM)
        {
            sprite = Resources.Load<Sprite>("Images/Common/icon_gem");
        }

        return sprite;
    }

    public static bool IsStringEmpty(string str)
    {
        if (str == null || str.Trim().Equals(""))
        {
            return true;
        }
        //
        return false;
    }

    #region FORMAT_NUMBER


    private static string FormatPrefixZero(long value)
    {
        if (value < 10)
        {
            return "0" + value;
        }
        else
        {
            return value.ToString();
        }
    }

    public static string FormatM(long value, bool roundUp = false)
    {
        //value = 10000012;
        //1m
        if (value < 1000000)
        {
            return FormatK(value, roundUp);
        }
        else
        {
            if (roundUp)
            {
                long tmp1 = value / 1000000;
                return FormatThreeDot(tmp1) + "M";
            }
            else
            {
                float tmp = value * 1f / 1000000;
                //
                bool isInteger = (tmp == Mathf.Floor(tmp));
                if (isInteger)
                {
                    return tmp + "M";
                }
                else
                {
                    string strM = tmp.ToString("0.00");
                    if (strM.EndsWith(".00") && strM.Length > 3)
                    {
                        strM = strM.Substring(0, strM.Length - 3);
                    }
                    //
                    return strM + "M";
                }
            }
        }
    }

    public static string FormatK(long value, bool roundUp = false)
    {
        if (value < 10000)
        {
            return FormatThreeDot(value);
        }
        else
        {
            if (roundUp)
            {
                long tmp1 = value / 1000;
                return FormatThreeDot(tmp1) + "K";
            }
            else
            {
                float tmp = value * 1f / 1000;
                //
                bool isInteger = (tmp == Mathf.Floor(tmp));
                if (isInteger)
                {
                    return tmp + "K";
                }
                else
                {
                    string strK = tmp.ToString("0.00");
                    if (strK.EndsWith(".00") && strK.Length > 3)
                    {
                        strK = strK.Substring(0, strK.Length - 3);
                    }
                    //
                    return strK + "K";
                }
            }
        }
    }

    public static string FormatVan(long value)
    {
        if (value < 1000000)
        {
            return FormatThreeDot(value);
        }
        else
        {
            long tmp = value / 10000;
            return FormatThreeDot(tmp) + " vạn";
        }
    }

    public static string FormatFloat(float value, int numberDigitAfterComma = 1)
    {
        if (numberDigitAfterComma <= 0)
        {
            return ((int)value).ToString();
        }
        else
        {
            string strFormat = "0.";
            for (int i = 0; i < numberDigitAfterComma; i++)
            {
                strFormat = strFormat + "0";
            }
            //
            return value.ToString(strFormat);
        }
    }

    public static string FormatThreeDot(long value)
    {
        //tam thoi bo di
        return value.ToString();
        //string sign = "";
        //if (value < 0) {
        //	sign = "-";
        //}
        ////
        //long absValue = value;
        //if (value < 0) {
        //	absValue *= -1;
        //}
        ////
        //string strValue = absValue.ToString ();
        //int length = strValue.Length;
        ////
        //int count = 0;
        //StringBuilder builder = new StringBuilder ();
        //for (int i = length - 1; i >= 0; i--) {
        //	builder.Insert (0, strValue [i]);
        //	count++;
        //	if (count == 3 && i > 0) {
        //		builder.Insert (0, ".");
        //		count = 0;
        //	}
        //}
        ////
        //return sign + builder.ToString ();
    }

    public static string FormatThousandSeperator(long value)
    {
        string sign = "";
        if (value < 0)
        {
            sign = "-";
        }
        //
        long absValue = value;
        if (value < 0)
        {
            absValue *= -1;
        }
        //
        string strValue = absValue.ToString();
        int length = strValue.Length;
        //
        int count = 0;
        StringBuilder builder = new StringBuilder();
        for (int i = length - 1; i >= 0; i--)
        {
            builder.Insert(0, strValue[i]);
            count++;
            if (count == 3 && i > 0)
            {
                builder.Insert(0, ".");
                count = 0;
            }
        }
        //
        return sign + builder.ToString();
    }

    #endregion

    public static GameObject LoadPrefab(string pathPrefab)
    {
        GameObject prefab = Resources.Load<GameObject>(pathPrefab);
        if (prefab != null)
            return prefab;
        else
        {
            Debug.LogError("------- No prefab :" + pathPrefab);
            return null;
        }
    }


    public static Sprite createSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public static GameObject LoadPrefabAndInstantiate(string pathPrefab, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(pathPrefab);
        if (prefab != null)
        {
            return GameObject.Instantiate(prefab, parent, false);
        }
        else
        {
            Debug.LogError("------- No prefab :" + pathPrefab);
            return null;
        }
    }


    public static GameObject FindCanvas()
    {
        return CanvasHelper.GetCanvas();
    }

    public static Vector2 ToVector2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static T RandomListValue<T>(List<T> lst)
    {
        if (lst == null || lst.Count == 0)
            return default(T);
        //
        int count = lst.Count;
        int rnd = Utils.RandomInt(0, count - 1);
        //
        return lst[rnd];
    }

    public static bool GetAChance(int percent)
    {
        int tmp = RandomInt(0, 100);
        return tmp < percent;
    }

    public static float RandomFloat(float from, float to)
    {
        return Random.Range(from, to);
    }

    public static int RandomInt(int from, int to)
    {
        if (to - from <= 1)
        {
            return from;
        }
        return Random.Range(from, to);
    }

    public static GameDirection ReverseDirection(GameDirection direction)
    {
        if (direction == GameDirection.LEFT)
            return GameDirection.RIGHT;
        else
            return GameDirection.LEFT;
    }

    public static void SetGoActiveState(GameObject go, bool active)
    {
        if (go != null)
        {
            go.SetActive(active);
        }
    }

    public static string Md5Sum(string input)
    {
        byte[] hashBytes = MD5CryptoServiceProvider.GetMd5Bytes(input);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public static List<object> GenListObject<T>(List<T> list)
    {
        List<object> lstObj = new List<object>();
        foreach (T t in list)
        {
            lstObj.Add(t);
        }
        //
        return lstObj;
    }

    public static List<T> FilterList<T>(List<T> list, List<int> lstIndexRemove)
    {
        List<T> ret = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (!lstIndexRemove.Contains(i))
            {
                ret.Add(list[i]);
            }
        }
        //
        return ret;
    }
    public static List<string> ListLongToListString(List<long> lstInput)
    {
        List<string> lstString = new List<string>();
        foreach (long l in lstInput)
        {
            lstString.Add(l.ToString());
        }
        //
        return lstString;
    }

    public static List<int> ListLongToListInt(List<long> lstInput)
    {
        List<int> lstInt = new List<int>();
        foreach (long l in lstInput)
        {
            lstInt.Add((int)l);
        }
        //
        return lstInt;
    }

    public static List<long> ListIntToListLong(List<int> lstInput)
    {
        List<long> lstLong = new List<long>();
        foreach (int i in lstInput)
        {
            lstLong.Add(i);
        }
        //
        return lstLong;
    }

    public static string ConvertListToArrayJson<T>(List<T> list)
    {
        if (list == null)
        {
            return "[]";
        }
        string arrList = "[";
        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
            {
                arrList = arrList + list[i].ToString() + ",";
            }
            else
            {
                arrList = arrList + list[i].ToString();
            }
        }
        arrList = arrList + "]";
        return arrList;
    }

    public static string ConvertListToString<T>(List<T> list)
    {
        string arrList = "[";
        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
            {
                arrList = arrList + list[i].ToString() + ",";
            }
            else
            {
                arrList = arrList + list[i].ToString();
            }
        }
        arrList = arrList + "]";
        return arrList;
    }

    public static void PrintList<T>(List<T> list, bool error = false)
    {
        string tmp = "";
        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count - 1)
            {
                tmp = tmp + list[i].ToString() + ",";
            }
            else
            {
                tmp = tmp + list[i].ToString();
            }
        }
        //
        if (error)
        {
            Debug.LogError(tmp);
        }
        else
        {
            Debug.Log(tmp);
        }
    }

    public static void PrintArrayInt(int[] list, bool isError = false)
    {
        string tmp = "";
        for (int i = 0; i < list.Length; i++)
        {
            if (i < list.Length - 1)
            {
                tmp = tmp + list[i].ToString() + ",";
            }
            else
            {
                tmp = tmp + list[i].ToString();
            }
        }
        //
        if (isError)
        {
            Debug.LogError(tmp);
        }
        else
        {
            Debug.Log(tmp);
        }
    }

    public static List<long> ConvertListIntToLong(List<int> lstInt)
    {
        List<long> ret = new List<long>();
        //
        foreach (int i in lstInt)
        {
            ret.Add(i);
        }
        //
        return ret;
    }
    //
    public static List<int> ConvertListLongToInt(List<long> lstLong)
    {
        List<int> ret = new List<int>();
        //
        foreach (long l in lstLong)
        {
            ret.Add((int)l);
        }
        //
        return ret;
    }

    public static T GetListValue<T>(List<T> lstValue, int index)
    {
        if (lstValue == null)
            return default(T);
        //
        if (lstValue.Count > index)
        {
            return lstValue[index];
        }
        return default(T);
    }

    public static void ShuffleList<T>(List<T> lstInput)
    {
        if (lstInput.Count <= 1)
        {
            return;
        }
        for (int i = 0; i < lstInput.Count; i++)
        {
            int idxRnd = RandomInt(1, lstInput.Count - 1);
            T tmp = lstInput[i];
            lstInput[i] = lstInput[idxRnd];
            lstInput[idxRnd] = tmp;
        }
    }

    public static List<T> CloneList<T>(List<T> lstInput)
    {
        List<T> ret = null;
        if (lstInput == null)
        {
            return ret;
        }
        //
        ret = new List<T>();
        for (int i = 0; i < lstInput.Count; i++)
        {
            ret.Add(lstInput[i]);
        }
        //
        return ret;
    }

    public static string GetLastPathElement(string pathInput)
    {
        if (string.IsNullOrEmpty(pathInput))
        {
            return "";
        }
        //
        string[] arrElm = pathInput.Split('/');
        //
        if (arrElm.Length > 0)
        {
            return arrElm[arrElm.Length - 1];
        }
        else
        {
            return "";
        }
    }

    public static void tweenScale(GameObject target, Vector3 to, float duration = 0.5f, iTween.EaseType easeType = iTween.EaseType.easeInOutBack, string tweenCompleteCallback = "", GameObject targetCallback = null)
    {
        Hashtable config = new Hashtable();
        config.Add("scale", to);
        config.Add("time", duration);
        config.Add("islocal", true);
        config.Add("ignoretimescale", false);
        config.Add("easetype", easeType);
        if (!string.IsNullOrEmpty(tweenCompleteCallback))
        {
            config.Add("onComplete", tweenCompleteCallback);
            config.Add("oncompletetarget", targetCallback);
        }
        //
        iTween.ScaleTo(target, config);
    }

    public static void tweenPosition(GameObject target, Vector3 to, float duration = 0.5f, iTween.EaseType easeType = iTween.EaseType.easeInOutBack, string tweenCompleteCallback = "", GameObject targetCallback = null)
    {
        Hashtable config = new Hashtable();
        config.Add("position", to);
        config.Add("time", duration);
        config.Add("islocal", true);
        config.Add("ignoretimescale", false);
        config.Add("easetype", easeType);
        if (!string.IsNullOrEmpty(tweenCompleteCallback))
        {
            config.Add("onComplete", tweenCompleteCallback);
            config.Add("oncompletetarget", targetCallback);
        }
        //
        iTween.MoveTo(target, config);
    }
    //Load Sprite Atlas
    public static Sprite LoadSprite(string nameAtlas, string nameSprite)
    {
        Sprite sprite = null;
        string path = "SpriteAtlas/";
       // string pathImgnull = "";
        path += nameAtlas;
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>(path);
        sprite = atlas.GetSprite(nameSprite);
        if (sprite != null)
        {
            return sprite;
        }
        else
        {
          //  sprite = Resources.Load<Sprite>(pathImgnull);
            Debug.LogError("Sprite is nUll");
            return sprite;
        }

    }
}
