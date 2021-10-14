using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNhapThep : BasePopup
{
    [SerializeField] Text m_TextAsTinhToan;
    [SerializeField] Dropdown m_DropDownSLT;
    [SerializeField] Dropdown m_DropDownPhi;
    [SerializeField] Text m_TextAsThucTe;

    private void InitData(float astt)
    {
        m_DropDownSLT.ClearOptions();
        m_DropDownPhi.ClearOptions();
        m_DropDownSLT.AddOptions(new List<string> { "4", "6", "8", "10" });
        m_DropDownPhi.AddOptions(new List<string> { "16", "18", "20", "22", "25" });
        m_TextAsTinhToan.text = astt.ToString();
        CaculatorAst(4, 16);
        m_DropDownSLT.onValueChanged.AddListener(delegate
        {
            CaculatorAst(float.Parse(m_DropDownSLT.options[m_DropDownSLT.value].text), float.Parse(m_DropDownPhi.options[m_DropDownPhi.value].text));
        });
        m_DropDownPhi.onValueChanged.AddListener(delegate
        {
            CaculatorAst(float.Parse(m_DropDownSLT.options[m_DropDownSLT.value].text), float.Parse(m_DropDownPhi.options[m_DropDownPhi.value].text));
        });
    }
    private void CaculatorAst(float sl, float thep)
    {
        if (sl == 0) sl = 4;
        if (thep == 0) thep = 16;
        m_TextAsThucTe.text = sl * Mathf.PI * Mathf.Pow(thep / 2, 2) / 100 + "";
    }
    public void ButtonSelectClick()
    {
        DataCaculator.boTriThep = new BoTriThep(int.Parse(m_DropDownSLT.options[m_DropDownSLT.value].text), int.Parse(m_DropDownPhi.options[m_DropDownPhi.value].text));
        PopupDraw2d.ShowUp();
    }
    public static PopupNhapThep ShowUp(float AsThucTe)
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupNhapThep");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupNhapThep pop = goPopup.GetComponent<PopupNhapThep>();
        pop.InitData(AsThucTe);
        return pop;
    }
}
