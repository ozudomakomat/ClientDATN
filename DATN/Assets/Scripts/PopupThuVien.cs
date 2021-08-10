using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupThuVien : BasePopup
{
    [SerializeField] VerticalPoolGroup m_PoolItem;
    [SerializeField] GameObject m_GoCellPrefab;
    private List<string> mylist = new List<string>(new string[] { "Cường độ bê tông", "Bảng tra cường độ thép", "Bảng tra thép tròn",
    "Bảng tra thép hình","Nội suy 1 chiều","Nội suy 2 chiều"});

    private void Awake()
    {
        m_PoolItem.howToUseCellData(delegate (GameObject go, object data)
        {
            ItemCellThuVien cell = go.GetComponent<ItemCellThuVien>();
            cell.SetData((ItemCellTVInfo)data);
            cell.callback = delegate (int id)
            {
                // show popup infos
                Debug.Log("id click = " + id);
            };
        });
    }
    protected override void Start()
    {
        if (m_GoCellPrefab != null)
        {
            m_GoCellPrefab.SetActive(false);
        }
        m_PoolItem.setAdapter(Utils.GenListObject(genData()));
    }

    // gen data
    private List<ItemCellTVInfo> genData()
    {
        List<ItemCellTVInfo> data = new List<ItemCellTVInfo>();
        for (int i = 0; i < mylist.Count; i++)
        {
            ItemCellTVInfo item = new ItemCellTVInfo(mylist[i], i);
            data.Add(item);
        }
        return data;
    }

    public override void ClosePopup()
    {
        m_GoContent.GetComponent<UITransition>().DoHide();
        Destroy(gameObject, 0.2f);
    }

    public static PopupThuVien ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupThuVien");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupThuVien pop = goPopup.GetComponent<PopupThuVien>();
        return pop;
    }
}
