using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBuffPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem efc_buffHeald;
    [SerializeField] ParticleSystem efc_dotfire;
    [SerializeField] GameObject efc_buffShell;
    private Coroutine m_coroutin;
    private bool m_IsPlayGame = false;

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventName.CHANGE_HEALD, PlayEffectBuffHeald);
        EventDispatcher.Instance.AddListener(EventName.PLAY_EFFECT_DOT, PlayEffectDot);
        EventDispatcher.Instance.AddListener(EventName.BUFF_SHELL, PlayBuffShell);
        EventDispatcher.Instance.AddListener(EventName.MANAGER_LOSE, InitLostGame);
        EventDispatcher.Instance.AddListener(EventName.MANAGER_STARTGAME, InitStarGame);
    }
    
    private void OnDisable()
    {
        
        EventDispatcher.Instance.RemoveListener(EventName.CHANGE_HEALD, PlayEffectBuffHeald);
        EventDispatcher.Instance.RemoveListener(EventName.PLAY_EFFECT_DOT, PlayEffectDot);
        EventDispatcher.Instance.RemoveListener(EventName.BUFF_SHELL, PlayBuffShell);
        EventDispatcher.Instance.RemoveListener(EventName.MANAGER_STARTGAME, InitStarGame);
    }

    private void PlayBuffShell(EventName key, object data)
    {
        bool active = (Active)data == Active.On ? true : false;
        efc_buffShell?.SetActive(active);
    }
    private void InitLostGame(EventName key, object data) {
        m_IsPlayGame = false;
        efc_buffHeald?.Stop();
        efc_dotfire?.Stop();
        efc_buffShell?.SetActive(false);
    }
    private void InitStarGame(EventName key, object data)
    {
        m_IsPlayGame = true;
        efc_buffHeald?.Stop();
        efc_dotfire?.Stop();
        efc_buffShell?.SetActive(false);
    }
    private void PlayEffectBuffHeald(EventName key, object data)
    {
        if((int)data>0)
        efc_buffHeald?.Play();
    }

    private void PlayEffectDot(EventName key, object data)
    {
        efc_dotfire.Play();
        if (m_coroutin == null) {
            m_coroutin = StartCoroutine(DotFire());
        } else
        {
            StopCoroutine(m_coroutin);
            m_coroutin = StartCoroutine(DotFire());
        }
    }

    IEnumerator DotFire()
    {
        int dame = -3;
        while (dame < 0 && m_IsPlayGame)
        {
            yield return new WaitForSeconds(1f);
            EventDispatcher.Instance.Dispatch(EventName.CHANGE_HEALD, -1);
            dame++;
        }
    }

    private void Update()
    {
        if (efc_buffShell.activeInHierarchy) {
            efc_buffShell?.transform.Rotate(0f, 0.0f, 0.5f);
        }
    }
}
