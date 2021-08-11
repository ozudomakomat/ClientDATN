using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTraThep : BasePopup
{
    [SerializeField] GameObject m_PanelTraThep;
    [SerializeField] Dropdown m_ConCrete;
    [SerializeField] Text m_TextRsn;
    [SerializeField] Text m_Textmddh;
    [SerializeField] Text m_TextCdcn;
    [SerializeField] Text m_TextCdcntd;
    [SerializeField] Text m_TextCdcntdd;
    List<string> m_DropConcrete = new List<string>();

    [System.Obsolete]
    private void OnDisable()
    {
        for (int i = 0; i < m_PanelTraThep.transform.GetChildCount(); i++)
        {
            m_PanelTraThep.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    [System.Obsolete]
    IEnumerator showItem()
    {
        for (int i = 0; i < m_PanelTraThep.transform.GetChildCount(); i++)
        {
            yield return new WaitForSeconds(0.05f);
            m_PanelTraThep.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    [System.Obsolete]
    private void Awake()
    {
        StartCoroutine(showItem());
        m_ConCrete.ClearOptions();
        DbManager.GetInstance().cdt.ForEach(item =>
        {
            m_DropConcrete.Add(item.name);
        });
        m_ConCrete.AddOptions(m_DropConcrete);
        SetValueCapDoBen(0);
    }
    public void SetValueCapDoBen(int id)
    {
        m_TextRsn.text = DbManager.GetInstance().cdt[id].rsn.ToString();
        m_Textmddh.text = DbManager.GetInstance().cdt[id].mddh.ToString();
        m_TextCdcn.text = DbManager.GetInstance().cdt[id].cdcn.ToString();
        m_TextCdcntd.text = DbManager.GetInstance().cdt[id].cdcntd.ToString();
        m_TextCdcntdd.text = DbManager.GetInstance().cdt[id].cdcntdd.ToString();
    }
    public void DropdownConCreteChange()
    {
        SetValueCapDoBen(m_ConCrete.value);
    }
    public static PopupTraThep ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupTraThep");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupTraThep pop = goPopup.GetComponent<PopupTraThep>();
        return pop;
    }
}
