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

    // caculator
    DataNoiBo data;
    private bool isLight = false;


    protected override void Awake()
    {
        base.Awake();
        data = DataCaculator.data;
        CaculatorImage();
    }

    protected override void Start()
    {
        base.Start();
        int cd_3 = data.L / 3;
        SetTextDim(cd_3, data.L);
        SetTextPhi("Ø8 a150", "Ø8 a250");
        SetTextPhiThep("Ø18");
        SetKichThuocCot(data.Cx, data.Cy);
    }
    private void CaculatorImage()
    {
        float width = m_ImgFxT1.rect.width;
        float op = (Mathf.Abs( m_ImgLeft.anchoredPosition.x) - Mathf.Abs(m_ImgThepDaiLeft.anchoredPosition.x))*2 ;
        float percent = (float)(data.Cy) / (float)data.Cx;
        int addHeight = (int)(width * percent);
        m_ImgRight.sizeDelta = new Vector2(3, addHeight);
        m_ImgLeft.sizeDelta = new Vector2(3, addHeight);
        m_ImgThepDaiLeft.sizeDelta = new Vector2(3, addHeight -op);
        m_ImgThepDaiRight.sizeDelta = new Vector2(3, addHeight - op);
        //
        Vector3 fxT = m_ImgFxT1.anchoredPosition;
        fxT.y = addHeight / 2;
        m_ImgFxT1.anchoredPosition = fxT;
        //
        fxT = m_ImgFxT2.anchoredPosition;
        fxT.y = (addHeight - op)/2;
        m_ImgFxT2.anchoredPosition = fxT;
        //
        fxT = m_ImgFxB1.anchoredPosition;
        fxT.y = -addHeight / 2;
        m_ImgFxB1.anchoredPosition = fxT;
        //
        fxT = m_ImgFxB2.anchoredPosition;
        fxT.y = -(addHeight - op)/2;
        m_ImgFxB2.anchoredPosition = fxT;
        //
        fxT = m_CotThepTop.anchoredPosition;
        fxT.y = (addHeight - op) / 2 - 7;
        m_CotThepTop.anchoredPosition = fxT;
        //
        fxT = m_CotThepBot.anchoredPosition;
        fxT.y = -(addHeight - op) / 2 + 6;
        m_CotThepBot.anchoredPosition = fxT;
        // dim right
        m_ImgDimRight.sizeDelta = new Vector2(3, addHeight + 30);
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
    public void ButtonDarkClick()
    {
        if (isLight)
        {
            m_Bg.color = Color.black;
            isLight = false;
        }
    }


    public static PopupDraw2d ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupDraw2d");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupDraw2d pop = goPopup.GetComponent<PopupDraw2d>();
        return pop;
    }
}
