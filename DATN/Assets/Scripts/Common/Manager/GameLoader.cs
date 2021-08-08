using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameLoader : MonoBehaviour
{
    [SerializeField] GameObject m_ButtonPause;

    private GameObject m_PopupEndGame, m_PopupPause;

    public float WidthScene
    {
        get
        {
            return (float)Screen.width / Screen.height * Camera.main.orthographicSize;
        }
    }

    public float HeightScene
    {
        get
        {
            return (float)Screen.height / Screen.width * Camera.main.orthographicSize;
        }
    }

    
    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventName.MANAGER_LOSE, InitLostGame);
        EventDispatcher.Instance.AddListener(EventName.MANAGER_STARTGAME, InitStartGame);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventName.MANAGER_LOSE, InitLostGame);
        EventDispatcher.Instance.RemoveListener(EventName.MANAGER_STARTGAME, InitStartGame);
    }

    private void InitStartGame(EventName key, object data)
    {
        StopAllCoroutines();
    }
    private void InitLostGame(EventName key, object data)
    {
    }
    private void Start()
    {
        EventDispatcher.Instance.Dispatch(EventName.MANAGER_STARTGAME, null);
        EventDispatcher.Instance.Dispatch(EventName.PLAY_BANNER_VIEW, null);
    }

    public void ButtonPauseClick()
    {
        m_ButtonPause.SetActive(false);
        EventDispatcher.Instance.Dispatch(EventName.MANAGER_CONTINUE, false);
        if (m_PopupPause == null)
        {
            m_PopupPause = PopupPause.ShowUp(delegate
            {
                m_ButtonPause.SetActive(true);
            });
        }
    }

}
