using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupDraw2d : BasePopup
{
    [SerializeField] Image m_Bg;
    [SerializeField] RectTransform m_PointMid;
    // cot tren
    [SerializeField] Text m_TextDim1;
    [SerializeField] Text m_TextDim2;
    [SerializeField] Text m_TextDim3;
    [SerializeField] Text m_TextChieuCaoCot;
    [SerializeField] Text m_TextCotDaiGiua;
    [SerializeField] Text m_TextCotDaiBenDuoi;
    [SerializeField] Text m_TextCotDaiBenTren;
    // cot duoi
    [SerializeField] Text m_CDCodDai;
    [SerializeField] Text m_CDThep;
    [SerializeField] Text m_CDKichThuocCx;
    [SerializeField] Text m_CDKichThuocCy;
    // draw
    [SerializeField] RectTransform m_ImgRight;
    [SerializeField] RectTransform m_ImgLeft;
    [SerializeField] RectTransform m_ImgThepDaiLeft;
    [SerializeField] RectTransform m_ImgThepDaiRight;
    [SerializeField] RectTransform m_ImgFxT1;
    [SerializeField] RectTransform m_ImgFxT2;
    [SerializeField] RectTransform m_ImgFxB1;
    [SerializeField] RectTransform m_ImgFxB2;
    [SerializeField] RectTransform m_CotThepTop;
    [SerializeField] RectTransform m_CotThepBot;
    //dim 
    [SerializeField] RectTransform m_ImgDimRight;
    // 10 thanh
    [SerializeField] RectTransform m_10_1;
    [SerializeField] RectTransform m_10_2;
    [SerializeField] RectTransform m_10_3;
    [SerializeField] RectTransform m_10_4;
    // 6 thanh
    [SerializeField] GameObject m_6;
    // 8 thanh
    [SerializeField] RectTransform m_8_1;
    [SerializeField] RectTransform m_8_2;
    // cau tao 
    [SerializeField] RectTransform cauTaoGiua;
    [SerializeField] RectTransform cauTaoGiua2;
    // Thong Ke
    [SerializeField] GameObject m_BtnThongKe;
    // caculator
    DataNoiBo data;
    private bool isLight = false;
    private float scale;

    protected override void Awake()
    {
        base.Awake();
        data = DataCaculator.data;
        CaculatorImage();
        ActiveThepDoc(DataCaculator.boTriThep.soluong);
    }

    protected override void Start()
    {
        base.Start();
        int cd_3 = data.L / 3;
        SetTextDim(cd_3, data.L);
        SetTextPhi("Ø8 a200", "Ø8 a300");
        SetTextPhiThep("Ø" + DataCaculator.boTriThep.phi);
        SetKichThuocCot(data.Cx, data.Cy);
    }
    private void CaculatorImage()
    {
        float width = m_ImgFxT1.rect.width;
        float op = (Mathf.Abs(m_ImgLeft.anchoredPosition.x) - Mathf.Abs(m_ImgThepDaiLeft.anchoredPosition.x)) * 2;
        float percent = (float)(data.Cy) / (float)data.Cx;
        int addHeight = (int)(width * percent);
        int line_cotDai = 2;
        int line_Cot = 3;
        m_ImgRight.sizeDelta = new Vector2(line_Cot, addHeight);
        m_ImgLeft.sizeDelta = new Vector2(line_Cot, addHeight);
        m_ImgThepDaiLeft.sizeDelta = new Vector2(line_cotDai, addHeight - op - 5);
        m_ImgThepDaiRight.sizeDelta = new Vector2(line_cotDai, addHeight - op - 5);
        //
        Vector3 fxT = m_ImgFxT1.anchoredPosition;
        fxT.y = addHeight / 2;
        m_ImgFxT1.anchoredPosition = fxT;
        //
        fxT = m_ImgFxT2.anchoredPosition;
        fxT.y = (addHeight - op) / 2;
        m_ImgFxT2.anchoredPosition = fxT;
        //
        fxT = m_ImgFxB1.anchoredPosition;
        fxT.y = -addHeight / 2;
        m_ImgFxB1.anchoredPosition = fxT;
        //
        fxT = m_ImgFxB2.anchoredPosition;
        fxT.y = -(addHeight - op) / 2;
        m_ImgFxB2.anchoredPosition = fxT;
        //
        fxT = m_CotThepTop.anchoredPosition;
        fxT.y = (addHeight - op) / 2 - 7;
        m_CotThepTop.anchoredPosition = fxT;
        //
        fxT = m_CotThepBot.anchoredPosition;
        fxT.y = -(addHeight - op) / 2 + 6;
        m_CotThepBot.anchoredPosition = fxT;

        // 8 thanh
        fxT = m_8_1.anchoredPosition;
        fxT.y = (addHeight - op) / 6f; 
        m_8_1.anchoredPosition = fxT;
        fxT = m_8_2.anchoredPosition;
        fxT.y = -(addHeight - op) / 6f;
        m_8_2.anchoredPosition = fxT;

        // dim right
        m_ImgDimRight.sizeDelta = new Vector2(line_Cot, addHeight + 30);
        // 10 thanh
        cauTaoGiua.sizeDelta = new Vector2(line_cotDai, addHeight - op - 10);
        cauTaoGiua2.sizeDelta = new Vector2(line_cotDai, addHeight - op - 10);
        fxT = m_10_1.anchoredPosition;
        fxT.x = width / 4;
        m_10_1.anchoredPosition = fxT;
        m_10_4.anchoredPosition = fxT;

        fxT = m_10_2.anchoredPosition;
        fxT.x = 115 - width / 4;
        m_10_2.anchoredPosition = fxT;
        m_10_3.anchoredPosition = fxT;
    }

    private void SetTextDim(int cd, int l)
    {
        m_TextDim2.text = (l - cd) + "";
        m_TextDim1.text = cd.ToString();
        m_TextDim3.text = cd.ToString();
        m_TextChieuCaoCot.text = l.ToString();
    }

    private void SetTextPhi(string phiGiua, string phi2ben)
    {
        m_TextCotDaiBenDuoi.text = phiGiua;
        m_TextCotDaiBenTren.text = phiGiua;
        m_TextCotDaiGiua.text = phi2ben;
        m_CDCodDai.text = phiGiua;
    }

    private void SetKichThuocCot(int cx, int cy)
    {
        m_CDKichThuocCx.text = cy.ToString();
        m_CDKichThuocCy.text = cx.ToString();
    }

    private void SetTextPhiThep(string phi)
    {
        m_CDThep.text = phi;
    }

    public void ButtonLightClick()
    {
        if (isLight == false)
        {
            m_Bg.color = Color.white;
            isLight = true;
        }
    }
    public void ButtonThongKeClick()
    {
        if (scale == 0 || scale == 1)
        {
            scale = 3f;
            m_BtnThongKe.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1.0f);
            return;
        }
        if (scale == 3)
        {
            scale = 1f;
            m_BtnThongKe.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1.0f);
            return;
        }
    }
    public void ButtonDarkClick()
    {
        if (isLight)
        {
            m_Bg.color = Color.black;
            isLight = false;
        }
    }

    private void ActiveThepDoc(int soluong)
    {
        switch (soluong)
        {
            case 4:
                {
                    DisableThepDoc();
                    break;
                }
            case 6:
                {
                    DisableThepDoc();
                    m_6.SetActive(true);
                    break;
                }
            case 8:
                {
                    DisableThepDoc();
                    m_8_1.gameObject.SetActive(true);
                    m_8_2.gameObject.SetActive(true);
                    break;
                }
            case 10:
                {
                    DisableThepDoc();
                    m_10_1.gameObject.SetActive(true);
                    m_10_2.gameObject.SetActive(true);
                    m_10_3.gameObject.SetActive(true);
                    m_10_4.gameObject.SetActive(true);
                    m_6.SetActive(true);
                    break;
                }
        }
    }

    private void DisableThepDoc()
    {
        m_10_1.gameObject.SetActive(false);
        m_10_2.gameObject.SetActive(false);
        m_10_3.gameObject.SetActive(false);
        m_10_4.gameObject.SetActive(false);
        m_6.SetActive(false);
        m_8_1.gameObject.SetActive(false);
        m_8_2.gameObject.SetActive(false);
    }


    public static PopupDraw2d ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupDraw2d");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupDraw2d pop = goPopup.GetComponent<PopupDraw2d>();
        return pop;
    }
}
