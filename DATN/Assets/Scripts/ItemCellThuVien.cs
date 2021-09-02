using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellThuVien : MonoBehaviour
{
    [SerializeField] Text m_TextGroupId;
    [SerializeField] Text m_TextBeTong;
    [SerializeField] Text m_TextThep;
    [SerializeField] Text m_TextN;
    [SerializeField] Text m_TextMx;
    [SerializeField] Text m_TextMy;
    [SerializeField] Text m_Texta;
    [SerializeField] Text m_Textb;
    [SerializeField] Text m_Texth;
    [SerializeField] Text m_Textl;
    [SerializeField] Text m_Textas;
    [SerializeField] Text m_Textmuy;

    public void SetData(History data)
    {
        m_TextGroupId.text = data.id.ToString();
        m_TextBeTong.text =  DbManager.GetInstance().cdbt[data.idBeTong].name;
         m_TextThep.text = DbManager.GetInstance().cdt[data.idThep].name;
        m_TextN.text = Math.Round(data.n, 2) + "";
        m_TextMx.text = Math.Round(data.mx, 2) + "";
        m_TextMy.text = Math.Round(data.my, 2) + "";
        m_Texta.text = data.a.ToString();
        m_Textb.text = data.b.ToString();
        m_Texth.text = data.h.ToString();
        m_Textl.text = data.l.ToString();
        m_Textas.text = Math.Round(data.ast, 2) + "";
        m_Textmuy.text = Math.Round(data.muy, 2) + "";
    }
}
