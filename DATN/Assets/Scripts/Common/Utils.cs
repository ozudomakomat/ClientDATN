using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using SimpleJSON;

public class Utils
{
   

 
    public static PaddingInfo CalculateSafePadding(Vector2 canvasSize)
    {
        int screenW = Screen.width;
        int screenH = Screen.height;
        //
        Rect rectSafe = Screen.safeArea;
        float paddingLeftPx = rectSafe.x;
        float paddingTopPx = rectSafe.y;
        //
        float paddingRightPx = screenW - paddingLeftPx - rectSafe.width;
        float paddingBotPx = screenH - paddingTopPx - rectSafe.height;
        float paddingLeft = (paddingLeftPx / screenW * canvasSize.x);
        float paddingRight = (paddingRightPx / screenW * canvasSize.x);
        float paddingTop = (paddingTopPx / screenH * canvasSize.y);
        float paddingBottom = (paddingBotPx / screenH * canvasSize.y);
        //
        string strDebug = "";
        strDebug += "ScreenW: " + screenW;
        strDebug += "\n" + "ScreenH: " + screenH;
        strDebug += "\n" + "RectSafeX: " + rectSafe.x;
        strDebug += "\n" + "RectSafeY: " + rectSafe.y;
        strDebug += "\n" + "RectSafeW: " + rectSafe.width;
        strDebug += "\n" + "RectSafeH: " + rectSafe.height;
        strDebug += "\n" + "=============";
        strDebug += "\n" + "PaddingLeft: " + paddingTopPx;
        strDebug += "\n" + "PaddingRight: " + paddingRightPx;
        strDebug += "\n" + "PaddingTop: " + paddingTopPx;
        strDebug += "\n" + "PaddingBottom: " + paddingBotPx;
        //
        PaddingInfo padding = new PaddingInfo(paddingLeft, paddingRight, paddingTop, paddingBottom);
        padding.m_StrDebug = strDebug;
        return padding;
    }
    public static long CalculateHeroPower(long attack, long armor, long hp)
    {
        return attack + armor + hp / 6;
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
    public static void ShowGO(GameObject go)
    {
        go.SetActive(true);
    }
    public static void HideGO(GameObject go)
    {
        go.SetActive(false);
    }
    public static IEnumerator IEFadeUI(Image img, float timeDelay, float duration, float alphaFrom = 0, float alPhaTo = 1)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, alphaFrom);
        yield return new WaitForSeconds(timeDelay);
        img.DOFade(alPhaTo, duration);
    }
    public static IEnumerator IEShowGO(GameObject go, float timeDelay)
    {
        HideGO(go);
        yield return new WaitForSeconds(timeDelay);
        ShowGO(go);
    }
    public static IEnumerator IEHideGO(GameObject go, float timeDelay)
    {
        ShowGO(go);
        yield return new WaitForSeconds(timeDelay);
        HideGO(go);
    }
    #region FORMAT_NUMBER

    public static string FormatHourFromSec(int sec)
    {
        int hour = sec / 3600;
        int tmp = sec % 3600;
        int min = tmp / 60;
        int sec2 = tmp % 60;
        //
        int day = hour / 24;
        //
        if (day > 0)
        {
            hour = hour % 24;
            return day + " ngày " + FormatPrefixZero(hour) + ":" + FormatPrefixZero(min) + ":" + FormatPrefixZero(sec2);
        }
        else
        {
            return FormatPrefixZero(hour) + ":" + FormatPrefixZero(min) + ":" + FormatPrefixZero(sec2);
        }
    }

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

    // tra ve random huong
    public static Direction RandomDirection()
    {
        int rd = RandomInt(1, 3);
        return rd == 1 ? Direction.Right : Direction.Left;
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
    public static AudioClip LoadAudioClip(string nameAudioClip)
    {
        AudioClip audioClip = Resources.Load<AudioClip>("AudioClip/" + nameAudioClip);
        if (audioClip != null)
            return audioClip;
        else
        {
            Debug.LogError("------- No audioClip :" + nameAudioClip);
            return null;
        }
    }
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


    public static float RandomFloat(float from, float to)
    {
        return Random.Range(from, to);
    }

    public static int RandomInt(int from, int to)
    {
        return Random.Range(from, to);
    }

    public static void SetGoActiveState(GameObject go, bool active)
    {
        if (go != null)
        {
            go.SetActive(active);
        }
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
    public static Transform CanvasPopupPanel
    {
        get
        {
            return GameObject.Find("CanvasPopup").transform;
        }
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
    public static IEnumerator IECountDownSecondsText(int seconds, Text result)
    {
        result.text = seconds.ToString();
        while (seconds >= 0)
        {
            yield return new WaitForSeconds(1f);
            seconds -= 1;
            result.text = seconds.ToString();
        }
    }
    public static GameObject createPrefabWithParent(string path, Transform parent)
    {
        GameObject go = UnityEngine.GameObject.Instantiate(createPrefab(path), parent) as GameObject;
        return go;
    }
    public static UnityEngine.Object createPrefab(string path)
    {
        return Resources.Load<UnityEngine.Object>(path);
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

    public static JSONArray ConvertStringToJsonArray(string json)
    {
        return JSONArray.Parse(json).AsArray;
    }

    public static List<int> ConvertStringToListInt(string json)
    {
        JSONArray js = ConvertStringToJsonArray(json);
        List<int> lstBn = new List<int>();
        if (json != null)
        {
            for (int i = 0; i < js.Count; i++)
            {
                lstBn.Add(js[i].AsInt);
            }
        }
        else
        {
            Debug.Log("---- Null list json array input");
        }
        return lstBn;

    }
    public static List<long> ConvertStringToListLong(string json)
    {
        JSONArray js = ConvertStringToJsonArray(json);
        List<long> lstBn = new List<long>();
        if (json != null)
        {
            for (int i = 0; i < js.Count; i++)
            {
                lstBn.Add(js[i].AsLong);
            }
        }
        else
        {
            Debug.Log("---- Null list json array input");
        }
        return lstBn;

    }
}
