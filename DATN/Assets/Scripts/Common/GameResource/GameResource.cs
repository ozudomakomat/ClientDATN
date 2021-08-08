using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameResource",fileName ="GameResource") ]
public class GameResource : ScriptableObject
{
    private static GameResource instance;
    public static GameResource Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<GameResource>("GameResource");
            }
            return instance;
        }
    }

    public DataReward dataRewards;
    public RewardSprite rewardSprites;
    public RewardSprite WingSprites;
    public PrefabsResource prefabs;
}
