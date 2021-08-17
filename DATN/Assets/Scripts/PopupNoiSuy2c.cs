using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNoiSuy2c : BasePopup
{
    [SerializeField] InputField x1;
    [SerializeField] InputField x2;
    [SerializeField] InputField x;
    [SerializeField] InputField y1;
    [SerializeField] InputField y2;
    [SerializeField] InputField y;
    [SerializeField] InputField q11;
    [SerializeField] InputField q12;
    [SerializeField] InputField q21;
    [SerializeField] InputField q22;
    [SerializeField] Text kq;

    public void ButtonCaculatorClick()
    {
        if (CheckInputData() == false) return;
        float X1 = float.Parse(x1.text);
        float X2 = float.Parse(x2.text);
        float X = float.Parse(x.text);
        float Y = float.Parse(y.text);
        float Y1 = float.Parse(y1.text);
        float Y2 = float.Parse(y2.text);
        float Q11 = float.Parse(q11.text);
        float Q12 = float.Parse(q12.text);
        float Q21 = float.Parse(q21.text);
        float Q22 = float.Parse(q22.text);
        float z1 = DataCaculator.NoiSuy1C(X1, X2, Q11, Q11, X);
        float z2 = DataCaculator.NoiSuy1C(X1, X2, Q21, Q22, X);
        float z3 = DataCaculator.NoiSuy1C(Y1, Y2, z1, z2, Y);
        kq.text = "Yns = " + z3;
    }
    public void ButtonDelClick()
    {
        x1.text = "";
        x2.text = "";
        x.text = "";
        y.text = "";
        y1.text = "";
        y2.text = "";
        q11.text = "";
        q12.text = "";
        q21.text = "";
        q22.text = "";
        Toast.ShowToast("Xóa thành công");
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
        if (y.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu x");
            return false;
        }
        if (q11.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu q11");
            return false;
        }
        if (q12.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu q12");
            return false;
        }
        if (q21.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu q21");
            return false;
        }
        if (q22.text == "")
        {
            Toast.ShowToast("Thiếu dữ liệu q22");
            return false;
        }
        return true;
    }
    public static PopupNoiSuy2c ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupNoiSuy2C");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupNoiSuy2c pop = goPopup.GetComponent<PopupNoiSuy2c>();
        return pop;
    }
}
