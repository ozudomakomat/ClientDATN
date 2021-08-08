using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEndGame : BasePopup
{
    [SerializeField] Text m_TextMyScore;
    [SerializeField] Text m_TextHightScore;

    private void InitData(int score) {
        m_TextMyScore.text = score.ToString();
        m_TextHightScore.text = "BEST: "+SaveData.Instance.GetHightScore();
    }
    public void ButtonReplayClick() {
        EventDispatcher.Instance.Dispatch(EventName.PLAY_INTERLEAVED, null);
        ClosePopup();
    }

    public void ButtonHomeClick() {
        LoadScene("HomeScene", true);
    }
    public static GameObject ShowUp(int score)
    {
        GameObject go = Utils.createPrefabWithParent("Prefabs/PopupEndGame", Utils.CanvasPopupPanel);
        go.GetComponent<PopupEndGame>().InitData(score);
        return go;
    }
    
}
