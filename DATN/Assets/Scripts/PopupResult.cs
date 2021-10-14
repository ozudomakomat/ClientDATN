using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupResult : BasePopup
{
    [SerializeField] Text m_TextGr;
    [SerializeField] Text m_TextlamdaX;
    [SerializeField] Text m_TextLamdaY;
    [SerializeField] Text m_Textx1;
    [SerializeField] Text m_Textm0;
    [SerializeField] Text m_TextM;
    [SerializeField] Text m_Texte0;
    [SerializeField] Text m_Texte1;
    [SerializeField] Text m_Textx;
    [SerializeField] Text m_TextAs;
    [SerializeField] Text m_TextMuy;
    public Action mAction;
    private float m_Ast;
    private void SetData(int gr, float lamdax, float lamday, float x1, float m0, float m, float e0, float e1, float x, float Ast, float muy)
    {
        m_TextGr.text = gr + "";
        m_TextlamdaX.text = Math.Round(lamdax, 2) + "";
        m_TextLamdaY.text = Math.Round(lamday, 2) + "";
        m_Textx1.text = Math.Round(x1, 2) + " mm";
        m_Textm0.text = Math.Round(m0, 2) + "";
        m_TextM.text = Math.Round(m, 2) + " kNm";
        m_Texte0.text = Math.Round(e0, 2) + " mm";
        m_Texte1.text = Math.Round(e1, 2) + " mm";
        m_Textx.text = Math.Round(x, 2) + " mm";
        m_TextAs.text = Math.Round(Ast, 2) + " mm2";
        m_TextMuy.text = Math.Round(muy, 2) +  " %";
        m_Ast = (float)Math.Round(Ast, 2);
    }

    public void ButtonNewCacuClick()
    {
        mAction?.Invoke();
        ClosePopup();
    }

    public void ButtonDraw2DClick() {
        PopupNhapThep.ShowUp(m_Ast);
    }
    
    public static PopupResult ShowUp(int gr, float lamdax, float lamday, float x1, float m0, float m, float e0, float e1, float x, float Ast, float muy)
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupResult");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupResult pop = goPopup.GetComponent<PopupResult>();
        pop.SetData(gr, lamdax, lamday, x1, m0, m, e0, e1, x, Ast, muy);
        return pop;
    }
}
