using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataReward", fileName = "DataReward")]
public class DataReward : ScriptableObject
{
    public List<AReward> m_data;

    public int GetDameWithType(TypeEffect type)
    {
        foreach (var i in m_data)
        {
            if (i.m_TypeEffect == type && type != TypeEffect.null_type)
                return i.m_Dame;
        }
        return 0;
    }
}

[Serializable]
public class AReward
{
    public int m_Id;
    public int m_Dame;
    public string m_Name;
    public string m_Des;
    public TypeEffect m_TypeEffect;
}

public enum TypeEffect
{
    luu_dan = 1, bay_no = 2, trieu_hoi = 3, bay_nay = 4,
    cong_mau = 5, giap_thien_than = 6, bom_khoi = 7, goi_dai_bang = 8, dong_dat = 9, thuoc_tang_luc = 10,
    thuoc_chay_cham = 11, trieu_hoi_cay = 12, bay_tam_thoi = 13, choang = 14, mua_qua = 15, tang_hinh = 16,
    tang_toc_qua = 17, bong_lan = 18, bomb = 19, long_nhot = 20, null_type = -1
}
