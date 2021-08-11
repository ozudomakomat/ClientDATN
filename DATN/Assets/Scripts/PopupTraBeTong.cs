using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTraBeTong : BasePopup
{
    [SerializeField] GameObject m_PanelTraBetong;
    [SerializeField] Dropdown m_ConCrete;
    [SerializeField] Text m_TextMac;
    [SerializeField] Text m_TextCdCn;
    [SerializeField] Text m_TextCdck;
    [SerializeField] Text m_Textmddh;
    [SerializeField] Text m_TextFcuBs;
    [SerializeField] Text m_TextFcuAci;
    List<string> m_DropConcrete = new List<string>();

    [System.Obsolete]
    private void OnDisable()
    {
        for (int i = 0; i < m_PanelTraBetong.transform.GetChildCount(); i++)
        {
            m_PanelTraBetong.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    [System.Obsolete]
    IEnumerator showItem()
    {
        for (int i = 0; i < m_PanelTraBetong.transform.GetChildCount(); i++)
        {
            yield return new WaitForSeconds(0.05f);
            m_PanelTraBetong.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    [System.Obsolete]
    private void Awake()
    {
        StartCoroutine(showItem());
        m_ConCrete.ClearOptions();
        DbManager.GetInstance().cdbt.ForEach(item =>
        {
            m_DropConcrete.Add(item.name);
        });
        m_ConCrete.AddOptions(m_DropConcrete);
        SetValueCapDoBen(0);
    }
    public void SetValueCapDoBen(int id)
    {
        m_TextMac.text = DbManager.GetInstance().cdbt[id].mac.ToString();
        m_TextCdCn.text = DbManager.GetInstance().cdbt[id].cdcn.ToString();
        m_TextCdck.text = DbManager.GetInstance().cdbt[id].cdck.ToString();
        m_Textmddh.text = DbManager.GetInstance().cdbt[id].mddh.ToString();
        m_TextFcuBs.text = DbManager.GetInstance().cdbt[id].fcubs.ToString();
        m_TextFcuAci.text = DbManager.GetInstance().cdbt[id].fcuaci.ToString();
    }
    public void DropdownConCreteChange()
    {
        SetValueCapDoBen(m_ConCrete.value);
    }
    public static PopupTraBeTong ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupTraBeTong");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupTraBeTong pop = goPopup.GetComponent<PopupTraBeTong>();
        return pop;
    }
}
