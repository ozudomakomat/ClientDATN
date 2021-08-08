using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DbManager : MonoBehaviour
{
    public List<CDBT> cdbt = new List<CDBT>();
    public List<CDT> cdt = new List<CDT>();
    private DbManager()
    {
        LoadAllDb();
    }
    private static DbManager instance;
    public static DbManager GetInstance()
    {
        if (instance == null)
        {
            instance = new DbManager();
        }
        return instance;

    }

    public void LoadAllDb()
    {
        LoadDbCDBT();
        LoadDbCDT();
    }
    private void LoadDbCDBT()
    {
        string dbFile = "db_cdbt";
        string json = ResourceHelper.LoadDbTextContent(dbFile);
        if (Utils.IsStringEmpty(json))
        {
            Debug.LogError("------ Error loading db file: " + dbFile);
            return;
        }
        //
        JSONArray arrAura = JSON.Parse(json).AsArray;
        cdbt.Clear();
        for (int i = 0; i < arrAura.Count; i++)
        {
            JSONClass jCd = arrAura[i].AsObject;
            CDBT itemCdbt = new CDBT();
            //
            itemCdbt.id = jCd["id"].AsInt;
            itemCdbt.name = jCd["name"].Value;
            itemCdbt.mac = jCd["mac"].AsInt;
            itemCdbt.cdcn = jCd["cdcn"].AsFloat;
            itemCdbt.cdck = jCd["cdck"].AsFloat;
            itemCdbt.mddh = jCd["mddh"].AsInt;
            itemCdbt.fcubs = jCd["fcubs"].AsFloat;
            itemCdbt.fcuaci = jCd["fcuaci"].AsFloat;
            cdbt.Add(itemCdbt);
        }
    }
    private void LoadDbCDT()
    {
        string dbFile = "db_cdt";
        string json = ResourceHelper.LoadDbTextContent(dbFile);
        if (Utils.IsStringEmpty(json))
        {
            Debug.LogError("------ Error loading db file: " + dbFile);
            return;
        }
        //
        JSONArray arrAura = JSON.Parse(json).AsArray;
        cdt.Clear();
        for (int i = 0; i < arrAura.Count; i++)
        {
            JSONClass jCd = arrAura[i].AsObject;
            CDT itemCdt = new CDT();
            //
            itemCdt.id = jCd["id"].AsInt;
            itemCdt.name = jCd["name"].Value;
            itemCdt.rsn = jCd["rsn"].AsInt;
            itemCdt.mddh = jCd["mddh"].AsInt;
            itemCdt.cdcn = jCd["cdcn"].AsInt;
            itemCdt.cdcntd = jCd["cdcntd"].AsInt;
            itemCdt.cdcntdd = jCd["cdcntdd"].AsInt;
            cdt.Add(itemCdt);
        }
    }
}
