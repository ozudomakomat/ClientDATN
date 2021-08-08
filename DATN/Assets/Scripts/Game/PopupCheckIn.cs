using UnityEngine;
using UnityEngine.UI;

public class PopupCheckIn : BasePopup
{
    [SerializeField] Text m_TextTimeCountDown;
    [SerializeField] GameObject m_GoItemGhosting;

    private void InitData() {
        m_GoItemGhosting.SetActive(true);
        m_TextTimeCountDown.text="Time remain: 23:01:10";
    }
    public static GameObject ShowUp() {
        GameObject go = Utils.createPrefabWithParent("Prefabs/PopupCheckIn", CanvasHelper.CanvasPopupPanel);
        go.GetComponent<PopupCheckIn>().InitData();
        return go;
    }
    public void ButtonClamClick() { 
        
    }
}
