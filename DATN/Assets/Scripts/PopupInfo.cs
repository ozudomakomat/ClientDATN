using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInfo : BasePopup
{

    public static PopupInfo ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupInfo");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupInfo pop = goPopup.GetComponent<PopupInfo>();
        return pop;
    }
}
