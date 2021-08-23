using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSteel : MonoEventHandler
{
    [SerializeField] InputField m_TextN;
    [SerializeField] InputField m_TextMx;
    [SerializeField] InputField m_TextMy;
    [SerializeField] InputField m_Texta;
    [SerializeField] InputField m_TextB;
    [SerializeField] InputField m_TextH;
    [SerializeField] InputField m_TextL;

    private float[] c3 = new float[] { 0.628f, 0.619f, 0.590f, 0.563f, 0.541f, 0.519f, 0.498f, 0.473f, 0.453f, 0.434f, 0.411f };
    private float[] c2 = new float[] { 0.660f, 0.65f, 0.623f, 0.595f, 0.573f, 0.552f, 0.530f, 0.505f, 0.485f, 0.465f, 0.442f };
    private float[] c1 = new float[] { 0.682f, 0.673f, 0.645f, 0.618f, 0.596f, 0.575f, 0.553f, 0.528f, 0.508f, 0.488f, 0.464f };

    [System.Obsolete]
    private void OnEnable()
    {
        StartCoroutine(showItem());
    }


    [System.Obsolete]
    private void OnDisable()
    {
        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
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

    public void ButtonCaculatorClick()
    {
        if (m_TextN.text == "") m_TextN.text = "1000";
        if (m_TextMx.text == "") m_TextMx.text = "30";
        if (m_TextMy.text == "") m_TextMy.text = "30";
        if (m_Texta.text == "") m_Texta.text = "30";
        if (m_TextB.text == "") m_TextB.text = "220";
        if (m_TextH.text == "") m_TextH.text = "400";
        if (m_TextL.text == "") m_TextL.text = "3600";
        int a = int.Parse(m_Texta.text);
        if (a <= 0)
        {
            Toast.ShowToast("a phải lớn hơn 0");
            return;
        }
        int Cy = int.Parse(m_TextB.text);
        if (Cy <= 0)
        {
            Toast.ShowToast("Cy phải lớn hơn 0");
            return;
        }
        int Cx = int.Parse(m_TextH.text);
        if (Cx <= 0)
        {
            Toast.ShowToast("Cx phải lớn hơn 0");
            return;
        }
        int L = int.Parse(m_TextL.text);
        if (L <= 0)
        {
            Toast.ShowToast("L phải lớn hơn 0");
            return;
        }
        float Mx = float.Parse(m_TextMx.text);
        float My = float.Parse(m_TextMy.text);
        float N = float.Parse(m_TextN.text);
        String urlConver = Utils.GetUrlCaculator(DataCaculator.GetInstance().groupId, DataCaculator.cdbt.id, DataCaculator.cdt.id, N,Mx,My,a,Cx,Cy,L);
        m_DataSender.SendDataCaculator(urlConver);
    }
    public override void ProcessKEvent(int eventId, object data)
    {
        base.ProcessKEvent(eventId, data);
        if (eventId == DataSender.CACULATOR)
        {
            byte[] bytes = (byte[])data;
            string strData = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("---- Data json: " + strData);
            try
            {
                List<int> lstAssetInfo = new List<int>();
                JSONArray jAsset = JSON.Parse(strData).AsArray;
                Debug.Log("jAsset = " + jAsset.ToString());
            }
            catch (System.Exception ex)
            {
                Toast.ShowToast(strData);
            }
        }
    }
    public void TinhToanCot(DataNoiBo tt)
    {
        CDBT cdbt = DataCaculator.cdbt;
        CDT cdt = DataCaculator.cdt;
        float lamdax, lamday, nx, ny, e0x, e0y, Ix, Iy, thetaX, thetaY, Ncrx, Ncry, Mxx, Myy, b, h, M1, M2;
        float h0, za, xi, x1, m0, M, e0, _As, e1, x, epl, Ast, Ks, u;
        // nhap vao chu mac dinh = 2.5
        Ks = 2.5f;
        xi = trabangxi(cdbt.id, cdt.id);
        lamdax = tt.L / tt.Cx;
        lamday = tt.L / tt.Cy;
        e0x = tt.Mx / tt.N;
        e0y = tt.My / tt.N;
        if (lamdax < 8)
        {
            nx = 1;
            Mxx = tt.N * e0x;
        }
        else
        {
            Ix = (tt.Cx ^ 3 * tt.Cy) / 12;
            thetaX = (float)((0.2 * e0x + 1.5 * tt.Cx) / (1.5 * e0x + tt.Cx));
            Ncrx = (float)(((2.5 * thetaX * cdbt.mddh * Ix) / (tt.L ^ 2)) / 10000);
            nx = 1 / (1 - (tt.N / Ncrx));
            Mxx = tt.N * nx * e0x;
        }

        if (lamday < 8)
        {
            ny = 1;
            Myy = tt.N * e0y;
        }
        else
        {
            Iy = (tt.Cy ^ 3 * tt.Cx) / 12;
            thetaY = (float)((0.2 * e0y + 1.5 * tt.Cy) / (1.5 * e0y + tt.Cy));
            Ncry = (float)(((2.5 * thetaY * cdbt.mddh * Iy) / (tt.L ^ 2)) / 10000);
            ny = 1 / (1 - (tt.N / Ncry));
            Myy = tt.N * ny * e0y;
        }

        if (Mxx / tt.Cx > Myy / tt.Cy)
        {
            b = tt.Cy;
            h = tt.Cx;
            M1 = Mxx;
            M2 = Myy;
        }
        else
        {
            h = tt.Cy;
            b = tt.Cx;
            M1 = Myy;
            M2 = Mxx;
        }
        h0 = h - tt.a;
        za = h - 2 * tt.a;
        x1 = tt.N / (cdbt.cdcn * 100 * b / 1000);
        if (x1 > h0) m0 = 0.4f;
        else
            m0 = (float)(1 - ((0.6 * x1) / (h0 / 1000)));
        M = M1 + m0 * M2 * (h / b);
        e0 = M / tt.N;
        e1 = (float)(e0 + 0.5 * (h / 1000) - (tt.a / 1000));
        if (x1 < xi * (h0 / 1000))
        {
            x = x1;
        }
        else
        {
            epl = e0 / (h0 / 1000);
            x = (xi + (1 - xi) / (1 + 50 * Mathf.Pow(epl, 2))) * (h0 / 1000); //m
        }
        _As = (tt.N * e1 - cdbt.cdcn * 100 * (b / 1000) * x * ((h0 / 1000) - 0.5f * x)) * 10000 / (cdt.cdcn * 100 * (za / 1000)); // cm2
        Ast = Ks * _As;
        u = Ast * 100 / ((b / 10) * (h / 10)); // %
        //Toast.ShowToast("Ast = " + Ast + " cm2 \n" + " muy% = " + u + " %");
        PopupResult.ShowUp("Ast = " + Ast + "cm2 \n \n muy % = " + u + " %");
    }

    public float trabangxi(int i, int j)
    {
        if (j == 0)
        {
            return (c1[i]);
        }
        else if (j == 1)
        {
            return (c2[i]);
        }
        else if (j == 2 || j == 3)
        {
            return (c3[i]);
        }
        return 0;
    }
}
