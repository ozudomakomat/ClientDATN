using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNoiSuy1C : BasePopup
{
    [SerializeField] InputField x1;
    [SerializeField] InputField x2;
    [SerializeField] InputField x;
    [SerializeField] InputField y1;
    [SerializeField] InputField y2;
    [SerializeField] Text y;

    public void ButtonCaculatorClick()
    {
        if (CheckInputData() == false) return;
        float X1 = float.Parse(x1.text);
        float X2 = float.Parse(x2.text);
        float X = float.Parse(x.text);
        float Y1 = float.Parse(y1.text);
        float Y2 = float.Parse(y2.text);
        float Y = DataCaculator.NoiSuy1C(X1,X2,Y1,Y2,X);
        y.text = "Yns = " + Y;
    }
    private bool CheckInputData()
    {
        if (x1.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu x1");
            return false;
        }
        if (x2.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu x2");
            return false;
        }
        if (x.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu x");
            return false;
        }
        if (y1.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu y1");
            return false;
        }
        if (y2.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu y2");
            return false;
        }
        return true;
    }
    public static PopupNoiSuy1C ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupNoiSuy1C");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupNoiSuy1C pop = goPopup.GetComponent<PopupNoiSuy1C>();
        return pop;
    }
}
