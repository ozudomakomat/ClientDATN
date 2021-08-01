using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JSONHelper
{
    //input = [1,2,3,4,5]
    public static List<long> ParseArrayLong(string strJsonArr)
    {
        List<long> lstRet = new List<long>();
        //
        if (Utils.IsStringEmpty(strJsonArr))
        {
            return lstRet;
        }
        //
        JSONArray jArr = JSON.Parse(strJsonArr).AsArray;
        for (int i = 0; i < jArr.Count; i++)
        {
            lstRet.Add(jArr[i].AsLong);
        }
        //
        return lstRet;
    }
}
