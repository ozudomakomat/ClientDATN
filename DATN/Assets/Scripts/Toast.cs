using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : BasePopup
{
    [SerializeField] Text m_Content;

    public void InitData(string content) {
        m_Content.text = content;
        StartCoroutine(AutoClose());
    }

    IEnumerator AutoClose() {
        yield return new WaitForSeconds(1f);
        ClosePopup();
    }

    public static Toast ShowUp(string content)
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupToast");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        Toast pop = goPopup.GetComponent<Toast>();
        pop.InitData(content);
        return pop;
    }
}
