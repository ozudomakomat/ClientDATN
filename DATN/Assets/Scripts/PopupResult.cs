using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupResult : BasePopup
{
    [SerializeField] Text m_TextContent;

    private void SetData(string content)
    {
        m_TextContent.text = content;
    }
    
    public static PopupResult ShowUp(string content)
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupResult");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupResult pop = goPopup.GetComponent<PopupResult>();
        pop.SetData(content);
        return pop;
    }
}
