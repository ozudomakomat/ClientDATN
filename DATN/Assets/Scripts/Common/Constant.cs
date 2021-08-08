using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public static string KEY_ADD_SPEED = "KEY_ADD_SPEED";
    public static string KEY_ITEM_IAP = "KEY_ITEM_IAP";
    public static string KEY_ITEM_ACTIVE = "KEY_ITEM_ACTIVE";

}

public enum Direction { Left = -1, Right = 1, Not = 0 }
public enum Active { On = 1, Off = 0 }
public enum GameStatus { Play, None, Lock,Star,End }
public enum TypeReward { Heald, Reward, RewardRain };


