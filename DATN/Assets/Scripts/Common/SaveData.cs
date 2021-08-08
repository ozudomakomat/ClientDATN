using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    #region Field
    private const string HIGHT_SCORE = "HIGHT_SCORE";
    private const string ACTIVE_SFX = "ACTIVE_SFX";
    private const string ACTIVE_MUSIC = "ACTIVE_MUSIC";
    public static float widthCanvas;
    public static float heigthCanvas;

    #endregion
    #region Sigerton
    private static SaveData instanse;

    public static SaveData Instance
    {
        get
        {
            if (instanse == null)
            {
                instanse = new SaveData();
            }
            return instanse;
        }
    }
    #endregion

    public void SaveHightScore(int HightScore)
    {
        PlayerPrefs.SetInt(HIGHT_SCORE, HightScore);
    }
    public int GetHightScore()
    {
        return PlayerPrefs.GetInt(HIGHT_SCORE);
    }
    public void SaveActiveSfx(bool ActiveSfx)
    {
        int sv = 0;
        if (ActiveSfx) sv = 1;
        PlayerPrefs.SetInt(ACTIVE_SFX, sv);
    }
    public bool IsActiveSfx()
    {
        return PlayerPrefs.GetInt(ACTIVE_SFX) == 1;
    }
    public void SaveActiveMusic(bool ActiveMusic)
    {
        int sv = 0;
        if (ActiveMusic) sv = 1;
        PlayerPrefs.SetInt(ACTIVE_MUSIC, sv);
    }
    public bool IsActiveMusic()
    {
        return PlayerPrefs.GetInt(ACTIVE_MUSIC) == 1;
    }
}
