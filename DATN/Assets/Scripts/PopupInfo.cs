using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfo : BasePopup
{
    [SerializeField] Text m_TextTop;
    [SerializeField] Text m_TextMid;
    [SerializeField] Text m_TextContent;
    [SerializeField] Text m_TextOutPut;

    private void InitData() {
        m_TextTop.text = "TRƯỜNG ĐẠI HỌC XÂY DỰNG \n KHOA CÔNG NGHỆ THÔNG TIN \n BỘ MÔN TIN HỌC XÂY DỰNG";
        m_TextMid.text = "ĐỒ ÁN TỐT NGHIỆP";
        m_TextContent.text = "Sinh viên thực hiện \n Mã số sinh viên\n Lớp\n Chuyên ngành\n Giảng viên hướng dẫn";
        m_TextOutPut.text = ": Nguyễn Đình Tú \n: 546859 \n: 59TH1 \n: Tin học xây dựng \n: PGS.TS.Trần Anh Bình";
    }
    public static PopupInfo ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupInfo");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupInfo pop = goPopup.GetComponent<PopupInfo>();
        pop.InitData();
        return pop;
    }
}
