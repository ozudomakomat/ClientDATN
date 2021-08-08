using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    private Animator anim;
    private string animState = "AnimState";
    private Coroutine m_stune;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().enabled = false;
        EventDispatcher.Instance.AddListener(EventName.STUN_PLAYER, PlayAnimation);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventName.STUN_PLAYER, PlayAnimation);
    }

    private void PlayAnimation(EventName key, object data)
    {
        if (m_stune == null)
        {
            m_stune = StartCoroutine(StartAnimStun((float)data));
        }
        else
        {
            StopCoroutine(m_stune);
            m_stune = StartCoroutine(StartAnimStun((float)data));
        }
    }

    IEnumerator StartAnimStun(float time)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        anim.SetInteger(animState, 1);
        yield return new WaitForSeconds(time);
        anim.SetInteger(animState, 0);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
