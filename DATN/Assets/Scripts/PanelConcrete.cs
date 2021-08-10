using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelConcrete : MonoBehaviour
{
    [SerializeField] Dropdown m_ConCrete;
    [SerializeField] Text m_Rb;
    [SerializeField] Text m_Eb;
    [SerializeField] Dropdown m_Steel;
    [SerializeField] Text m_Rs;
    [SerializeField] Text m_Rsc;
    [SerializeField] Text m_Es;

    List<string> m_DropConcrete = new List<string>();
    List<string> m_DropSteel = new List<string>();
    int idConcree = 0;
    int idSteel = 0;

    [System.Obsolete]
    private void OnDisable()
    {
        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        m_ConCrete.ClearOptions();
        m_Steel.ClearOptions();
        DbManager.GetInstance().cdbt.ForEach(item =>
        {
            m_DropConcrete.Add(item.name);
        });
        DbManager.GetInstance().cdt.ForEach(item =>
        {
            m_DropSteel.Add(item.name);
        });

    }

    private void Start()
    {
        m_ConCrete.AddOptions(m_DropConcrete);
        m_Steel.AddOptions(m_DropSteel);
        SetValueCapDoBen(0);
        SetValueSteel(0);
    }


    [System.Obsolete]
    private void OnEnable()
    {
        StartCoroutine(showItem());
    }

    [System.Obsolete]
    IEnumerator showItem()
    {
        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void DropdownConCreteChange()
    {
        SetValueCapDoBen(m_ConCrete.value);
    }
    public void DropdownSteelChange()
    {
        SetValueSteel(m_Steel.value);
    }
    public void SetValueCapDoBen(int id)
    {
        idConcree = id;
        m_Rb.text = DbManager.GetInstance().cdbt[id].cdcn.ToString();
        m_Eb.text = DbManager.GetInstance().cdbt[id].mddh.ToString();
    }
    public void SetValueSteel(int id)
    {
        idSteel = id;
        m_Rs.text = DbManager.GetInstance().cdt[id].cdcn.ToString();
        m_Rsc.text = DbManager.GetInstance().cdt[id].cdcntd.ToString();
        m_Es.text = DbManager.GetInstance().cdt[id].mddh.ToString();
    }
}
