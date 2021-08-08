using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EventName
{
    NONE,

    #region Game Specific
    MANAGER_WIN,
    MANAGER_INIT,
    MANAGER_LOSE,
    MANAGER_REPLAY,
    MANAGER_NEXT,
    MANAGER_STARTGAME,
    MANAGER_CONTINUE,

    SFX_CHANGE,
    SOUND_CHANGE,
    #endregion

    #region Game
    CHANGE_HEALD,
    COLLECT_REWARDS,
    BUFF_SHELL,
    CHANGE_SPEED_REWARD,
    AUTO_ACS_SPEED_REWARD,
    STUN_PLAYER,
    PLAY_EFFECT_DOT,
    MOVE,
    JUMP,
    SHAKE_CAMERA_UP_DOWN,
    SHAKE_CAMERA_UP,
    #endregion

    #region Ads vs IAP
    PLAY_BANNER_VIEW,
    PLAY_INTERLEAVED,
    #endregion
}

public class EventTypeComparer : IEqualityComparer<EventName>
{
    public bool Equals(EventName x, EventName y)
    {
        return x == y;
    }

    public int GetHashCode(EventName t)
    {
        return (int)t;
    }
}
