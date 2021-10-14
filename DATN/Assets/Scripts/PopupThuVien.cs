using Newtonsoft.Json;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupThuVien : BasePopup
{
    [SerializeField] VerticalPoolGroup m_PoolItem;
    [SerializeField] GameObject m_GoCellPrefab;
    [SerializeField] GameObject PanelList;
    [SerializeField] GameObject PanelInput;
    [SerializeField] InputField m_GroupId;

    //
    private const int INDEX_PANEL_INPUT = 0;
    private const int INDEX_PANEL_LIST = 1;

    protected override void Awake()
    {
        base.Awake();
        ShowTabById(INDEX_PANEL_INPUT);
        m_PoolItem.howToUseCellData(delegate (GameObject go, object data)
        {
            ItemCellThuVien cell = go.GetComponent<ItemCellThuVien>();
            cell.SetData((History)data);
        });
    }
    private void ShowTabById(int id)
    {
        PanelList.SetActive(false);
        PanelInput.SetActive(false);
        if (id == INDEX_PANEL_INPUT)
        {
            PanelInput.SetActive(true);
        }
        else
        {
            PanelList.SetActive(true);
        }
    }
    public void ButtonBackClick()
    {
        ShowTabById(INDEX_PANEL_INPUT);
    }

    protected override void Start()
    {
        base.Start();
        if (m_GoCellPrefab != null)
        {
            m_GoCellPrefab.SetActive(false);
        }

    }

    public void ButtonTraCuuClick()
    {
        if (string.IsNullOrEmpty(m_GroupId.text))
        {
            Toast.ShowToast("Group id không được để trống");
            return;
        }

        int groupId = int.Parse(m_GroupId.text);
        m_DataSender.SendDataGetHistory(groupId);
    }

    public override void ProcessKEvent(int eventId, object data)
    {
        base.ProcessKEvent(eventId, data);
        if (eventId == DataSender.HISTORY)
        {
            byte[] bytes = (byte[])data;
            string strData = System.Text.Encoding.UTF8.GetString(bytes);
            try
            {
                JSONArray js = Utils.ConvertStringToJsonArray(strData);
                List<string> lstr = new List<string>();
                if (strData != null)
                {
                    for (int i = 0; i < js.Count; i++)
                    {
                        lstr.Add(js[i].Value);
                    }
                }
                if (lstr.Count <= 0)
                {
                    Toast.ShowToast("Không có dữ liệu");
                    return;
                }
                List<History> historys = new List<History>();
                for (int i = 0; i < lstr.Count; i += 13)
                {
                    History his = new History();
                    his.id = int.Parse(lstr[i]);
                    his.idBeTong = int.Parse(lstr[i + 1]);
                    his.idThep = int.Parse(lstr[i + 2]);
                    his.n = float.Parse(lstr[i + 3]);
                    his.mx = float.Parse(lstr[i + 4]);
                    his.my = float.Parse(lstr[i + 5]);
                    his.a = int.Parse(lstr[i + 6]);
                    his.b = int.Parse(lstr[i + 7]);
                    his.h = int.Parse(lstr[i + 8]);
                    his.l = int.Parse(lstr[i + 9]);
                    his.ast = float.Parse(lstr[i + 10]);
                    his.muy = float.Parse(lstr[i + 11]);
                    his.date ="Ngày tạo : "+ Utils.converTickToTime(long.Parse(lstr[i + 12]));
                    historys.Add(his);
                } 
                m_PoolItem.setAdapter(Utils.GenListObject(historys));
                ShowTabById(INDEX_PANEL_LIST);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }

    public static PopupThuVien ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupThuVien");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupThuVien pop = goPopup.GetComponent<PopupThuVien>();
        return pop;
    }
}
