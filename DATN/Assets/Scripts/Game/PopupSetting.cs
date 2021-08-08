using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopup
{
    [SerializeField] RewardSprite m_SprButtons;
    [SerializeField] Image m_ImgButtonSfx;
    [SerializeField] Image m_ImgButtonMusic;

    private void Init()
    {
        SetImgButtonMusic();
    }
    public void ButtonChangeMusicClick()
    {
        EventDispatcher.Instance.Dispatch(EventName.SOUND_CHANGE, null);
        SetImgButtonMusic();
    }
    public void ButtonChangeSfxClick()
    {
        EventDispatcher.Instance.Dispatch(EventName.SFX_CHANGE, null);
        SetImgButtonMusic();
    }
    private void SetImgButtonMusic()
    {
        if (SaveData.Instance.IsActiveSfx())
            m_ImgButtonSfx.sprite = m_SprButtons.listSprite[1];
        else
            m_ImgButtonSfx.sprite = m_SprButtons.listSprite[0];
        if (SaveData.Instance.IsActiveMusic())
            m_ImgButtonMusic.sprite = m_SprButtons.listSprite[3];
        else
            m_ImgButtonMusic.sprite = m_SprButtons.listSprite[2];
        m_ImgButtonSfx.SetNativeSize();
        m_ImgButtonMusic.SetNativeSize();
    }
    
    public static GameObject ShowUp()
    {
        GameObject go = Utils.createPrefabWithParent("Prefabs/PopupSetting", CanvasHelper.CanvasPopupPanel);
        go.GetComponent<PopupSetting>().Init();
        return go;
    }
}
