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
    public Action m_ReCacu;

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
        if (m_TextN.text == "") m_TextN.text = "1200";
        if (m_TextMx.text == "") m_TextMx.text = "300";
        if (m_TextMy.text == "") m_TextMy.text = "156";
        if (m_Texta.text == "") m_Texta.text = "40";
        if (m_TextB.text == "") m_TextB.text = "600";
        if (m_TextH.text == "") m_TextH.text = "400";
        if (m_TextL.text == "") m_TextL.text = "4000";
        int a = int.Parse(m_Texta.text);
        if (a <= 0)
        {
            Toast.ShowToast("a phải lớn hơn 0");
            return;
        }
        int Cy = int.Parse(m_TextH.text);
        if (Cy <= 0)
        {
            Toast.ShowToast("Cy phải lớn hơn 0");
            return;
        }
        int Cx = int.Parse(m_TextB.text);
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
        DataCaculator.data = new DataNoiBo(N, Mx, My, a, Cx, Cy, L);
        m_DataSender.SendDataCaculator(DataCaculator.GetInstance().groupId, DataCaculator.cdbt.id, DataCaculator.cdt.id, N, Mx, My, a, Cx, Cy, L);
    }
    public override void ProcessKEvent(int eventId, object data)
    {
        base.ProcessKEvent(eventId, data);
        if (eventId == DataSender.CACULATOR)
        {
            byte[] bytes = (byte[])data;
            string strData = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(strData);
            try
            {
                List<int> lstAssetInfo = new List<int>();
                JSONClass jAsset = JSON.Parse(strData).AsObject;
                JSONClass js = jAsset["data"].AsObject;
                int groupId = js["groupId"].AsInt;
                float lamdax = js["lamdax"].AsFloat;
                float lamday = js["lamday"].AsFloat;
                float x1 = js["x1"].AsFloat;
                float m0 = js["m0"].AsFloat;
                float m = js["m"].AsFloat;
                float e0 = js["e0"].AsFloat;
                float e1 = js["e"].AsFloat;
                float x = js["x"].AsFloat;
                float Ast = js["Ast"].AsFloat;
                float muy = js["muy"].AsFloat;

                PopupResult rs = PopupResult.ShowUp(groupId, lamdax, lamday, x1, m0, m, e0, e1, x, Ast, muy);
                rs.mAction += delegate
                {
                    m_ReCacu?.Invoke();
                };
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}
