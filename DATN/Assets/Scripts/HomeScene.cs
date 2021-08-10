using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("cdt 0 = " + DbManager.GetInstance().cdt[0].rsn);
        
    }

    #region  Button Click
    public void ButtonMenuClick()
    {
        PopupThuVien.ShowUp();
    }
    public void ButtonInfoClick()
    {
        PopupInfo.ShowUp();
    }
    public void ButtonCaculatorClick()
    {
        PopupCaculator.ShowUp();
    }
    #endregion
}
