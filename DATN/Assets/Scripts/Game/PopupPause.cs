using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPause : BasePopup
{
    public System.Action CloseCallBack = null;
    private void Init(System.Action closeCB)
    {
        Time.timeScale = 0;
        CloseCallBack = closeCB;
    }
    public void ButtonMenuClick()
    {
        Time.timeScale = 1;
        LoadScene("HomeScene");
        ClosePopupCB();
    }
    public void ButtonReNewGameClick()
    {
        Time.timeScale = 1;
        EventDispatcher.Instance.Dispatch(EventName.MANAGER_STARTGAME, null);
        ClosePopupCB();
    }
    public void ButtonReGameClick()
    {
        Time.timeScale = 1;
        EventDispatcher.Instance.Dispatch(EventName.MANAGER_CONTINUE, true);
        ClosePopupCB();
    }
    private void ClosePopupCB()
    {
        Destroy(this.gameObject);
        if (CloseCallBack != null) CloseCallBack.Invoke();
    }
    public static GameObject ShowUp(System.Action closeCB)
    {
        GameObject go = Utils.createPrefabWithParent("Prefabs/PopupPause", CanvasHelper.CanvasPopupPanel);
        go.GetComponent<PopupPause>().Init(closeCB);
        return go;
    }

}
