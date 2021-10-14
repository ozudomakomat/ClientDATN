using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelThongKe : MonoBehaviour
{
    // text hinh dang kt
    [SerializeField] Text m_Cot2_1;
    [SerializeField] Text m_Cot2_2;
    [SerializeField] Text m_Cot2_3;
    [SerializeField] Text m_Cot2_4;
    [SerializeField] Text m_Cot2_5;
    [SerializeField] Text m_Cot2_6;
    // text phi
    [SerializeField] Text m_Phi;
    [SerializeField] Text m_PhiCt;
    // so thanh
    [SerializeField] Text m_soThanh_1;
    [SerializeField] Text m_soThanh_2;
    [SerializeField] Text m_soThanh_3;
    [SerializeField] Text m_soThanh_4;
    // tong chieu dai
    [SerializeField] Text m_chieudai_1;
    [SerializeField] Text m_chieudai_2;
    [SerializeField] Text m_chieudai_3;
    [SerializeField] Text m_chieudai_4;
    [SerializeField] Text m_chieudai_5;
    [SerializeField] Text m_chieudai_6;
    // khoi luong
    [SerializeField] Text m_khoiLuong_1;
    [SerializeField] Text m_khoiLuong_2;
    [SerializeField] Text m_khoiLuong_3;
    [SerializeField] Text m_khoiLuong_4;
    [SerializeField] Text m_khoiLuong_5;
    [SerializeField] Text m_khoiLuong_6;
    // thep cau tao
    [SerializeField] GameObject m_GoCauTao1;
    [SerializeField] GameObject m_GoCauTao2;

    private void Start()
    {
        int a1 = 300;
        int a2 = 200;
        m_Cot2_1.text = (DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) + "";
        m_Cot2_2.text = (DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) + "";
        m_Cot2_3.text = (DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) + "";
        m_Cot2_4.text = DataCaculator.data.L.ToString();
        m_Cot2_5.text = DataCaculator.data.L.ToString();
        m_Cot2_6.text = DataCaculator.data.Cx.ToString();
        m_Phi.text = DataCaculator.boTriThep.phi.ToString();
        m_PhiCt.text = DataCaculator.boTriThep.phi.ToString();
        // so thanh
        m_soThanh_1.text = ((int)(DataCaculator.data.L / (2 * a1))).ToString();
        m_soThanh_2.text = ((int)(DataCaculator.data.L / (4 * a2))).ToString();
        m_soThanh_3.text = ((int)(DataCaculator.data.L / (4 * a2))).ToString();
        m_soThanh_4.text = DataCaculator.boTriThep.soluong.ToString();
        // tong chieu dai
        m_chieudai_1.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (2 * a1))) + "";
        m_chieudai_2.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (4 * a2))) + "";
        m_chieudai_3.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (4 * a2))) + "";
        m_chieudai_4.text = (DataCaculator.boTriThep.soluong * DataCaculator.data.L) + "";
        m_chieudai_5.text = (DataCaculator.data.L * 2) + "";
        m_chieudai_6.text = (DataCaculator.data.Cy) + "";
        // khoi luong 
        m_khoiLuong_1.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (2 * a1)) * getHeSoKg(8)) + "";
        m_khoiLuong_2.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (4 * a2)) * getHeSoKg(8)) + "";
        m_khoiLuong_3.text = ((DataCaculator.data.Cy * 2 + DataCaculator.data.Cx * 2) * (int)(DataCaculator.data.L / (4 * a2)) * getHeSoKg(8)) + "";
        m_khoiLuong_4.text = (DataCaculator.boTriThep.soluong * DataCaculator.data.L * getHeSoKg(DataCaculator.boTriThep.phi)) + "";
        m_khoiLuong_5.text = (2 * DataCaculator.data.L * getHeSoKg(DataCaculator.boTriThep.phi)) + "";
        m_khoiLuong_6.text = (DataCaculator.data.Cy * getHeSoKg(8)) + "";
        if (DataCaculator.data.Cy > 500)
        {
            m_GoCauTao1.SetActive(true);
            m_GoCauTao2.SetActive(true);
        }
        else
        {
            m_GoCauTao1.SetActive(false);
            m_GoCauTao2.SetActive(false);
        }
    }
    private float getHeSoKg(int phi)
    {
        switch (phi)
        {
            case 6: return 0.222f;
            case 8: return 0.395f;
            case 10: return 0.617f;
            case 12: return 0.883f;
            case 14: return 1.208f;
            case 16: return 1.578f;
            case 18: return 1.998f;
            case 20: return 2.466f;
            case 22: return 2.984f;
            case 25: return 3.85f;
            case 28: return 4.83f;
            case 30: return 5.52f;
            case 32: return 6.31f;
        }

        return 0;
    }
}
