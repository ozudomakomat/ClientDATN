using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCaculator : MonoBehaviour
{
    private static DataCaculator instance;
    public static DataCaculator GetInstance()
    {
        if (instance == null)
        {
            instance = new DataCaculator();
        }
        return instance;
    }
    public static CDBT cdbt;
    public static CDT cdt;

    public static float NoiSuy1C(float x1, float x2, float y1, float y2,float x)
    {
        return y2 + (y1 - y2) * (x - x2) / (x1 - x2);
    }
}
