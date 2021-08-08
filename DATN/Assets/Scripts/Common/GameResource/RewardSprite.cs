using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "RewardSprite", fileName = "RewardSprite")]
public class RewardSprite : ScriptableObject
{
    public List<Sprite> listSprite;

    public Sprite RandomSprite()
    {
        return listSprite[Utils.RandomInt(0, listSprite.Count)];
    }
}