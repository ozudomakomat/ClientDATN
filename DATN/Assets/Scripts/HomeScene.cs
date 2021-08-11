using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(640, 1136, false);

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
