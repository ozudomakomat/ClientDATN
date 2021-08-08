using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    [HideInInspector] public Direction m_MoveDirection = Direction.Left;
    private float m_SpeedRewardFLy;
    [SerializeField] float m_SpeedRewardRain = 8f;
    [SerializeField] TypeReward typeReward = TypeReward.Reward;

    private TrailRenderer trial;
    private float m_PosSpeed = 2f;
    private Coroutine ChangeSpeed;
    private bool m_IsInitEnemy=true;

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventName.CHANGE_SPEED_REWARD, ChangeSpeedReward);
        EventDispatcher.Instance.AddListener(EventName.MANAGER_LOSE, Manager_LostGame);
        EventDispatcher.Instance.AddListener(EventName.MANAGER_STARTGAME, Manager_StarGame);
        if (typeReward == TypeReward.Reward || typeReward == TypeReward.RewardRain)
        {
            int rdIndex = Utils.RandomInt(1, GameResource.Instance.rewardSprites.listSprite.Count);
            SetSprite(GameResource.Instance.rewardSprites.RandomSprite());
            trial = this.GetComponent<TrailRenderer>();
            this.trial.startColor = new Color(Utils.RandomFloat(0, 1f), Utils.RandomFloat(0, 1f), Utils.RandomFloat(0, 1f));
            this.trial.endColor = Color.white;
        }
    }
    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventName.CHANGE_SPEED_REWARD, ChangeSpeedReward);
        EventDispatcher.Instance.RemoveListener(EventName.MANAGER_LOSE, Manager_LostGame);
        EventDispatcher.Instance.RemoveListener(EventName.MANAGER_STARTGAME, Manager_StarGame);
    }

    public void SetBaseSpeed(float speed)
    {
        m_SpeedRewardFLy = speed;
        m_PosSpeed = speed;
    }

    private void Manager_LostGame(EventName key, object data)
    {
        m_IsInitEnemy = false;
        GetComponent<BoxCollider2D>().enabled = false;
        m_SpeedRewardFLy = 0;
        if (this.gameObject != null)
            Destroy(this.gameObject);
    }
    private void Manager_StarGame(EventName key, object data)
    {
        if (this.gameObject != null)
            Destroy(this.gameObject);
        GetComponent<BoxCollider2D>().enabled = true;
        m_IsInitEnemy = true;
        m_SpeedRewardFLy = m_PosSpeed;
    }
    private void ChangeSpeedReward(EventName key, object data)
    {
        if (typeReward == TypeReward.Reward)
        {
            if (ChangeSpeed == null)
            {
                ChangeSpeed = StartCoroutine(ChangeSpeedReward());
            }
            else
            {
                StopCoroutine(ChangeSpeed);
                ChangeSpeed = StartCoroutine(ChangeSpeedReward());
            }
        }
    }


    IEnumerator ChangeSpeedReward()
    {
        m_SpeedRewardFLy = m_PosSpeed + 1;
        trial.enabled = true;
        yield return new WaitForSeconds(5f);
        m_SpeedRewardFLy = m_PosSpeed;
        trial.enabled = false;
    }
    private void Update()
    {
        if (m_IsInitEnemy == false) return;
        if (typeReward == TypeReward.Reward || typeReward == TypeReward.Heald)
            transform.position += new Vector3((int)m_MoveDirection * m_SpeedRewardFLy * Time.deltaTime, 0f, 0f);
    }

    public void SetDirection(Direction type)
    {
        m_MoveDirection = type;
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}

