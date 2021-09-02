using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : BaseScene
{
    protected override void Start()
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
    public void ButtonLichSuTinhToanClick() {
        PopupThuVien.ShowUp();
    }
    public void ButtonCaculatorClick()
    {
        PopupCaculator.ShowUp();
    }
    public void ButtonTraThepClick()
    {
        PopupTraThep.ShowUp();
    }
    public void ButtonTraBetongClick()
    {
        PopupTraBeTong.ShowUp();
    }
    public void ButtonNoiSuy1CClick()
    {
        PopupNoiSuy1C.ShowUp();
    }
    public void ButtonNoiSuy2CClick()
    {
        PopupNoiSuy2c.ShowUp();
    }
    #endregion

 
}
