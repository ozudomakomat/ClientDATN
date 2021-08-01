using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    //ngon ngu mac dinh khi chay game
    //neu la tieng anh thi client UI lay tieng anh va
    //server set url tieng anh cua mr truong
    public static Language DEFAULT_LANGUAGE = Language.VI;
    public static bool IsInitLanguage = false;
    //ngon ngu khi nguoi choi chuyen qua lai run-time
    public static Language LANGUAGE = Language.VI;
    //
#if UNITY_IOS
    public static bool TAI_TRUC_TIEP = false;
#elif UNITY_ANDROID
    public static bool TAI_TRUC_TIEP = false;
#else
    public static bool TAI_TRUC_TIEP = false;
#endif

    //
    public static int level = 0;

#if UNITY_EDITOR
    public static bool TEST_TIME_OUT = false;
#else
    public static bool TEST_TIME_OUT = false;
#endif

    public const int LEVEL_MAX = 1000;
#if UNITY_EDITOR || UNITY_STANDALONE
    public static bool USE_GOSU_SDK = false;
#else
    public static bool USE_GOSU_SDK = true;
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
    public static bool LOAD_BATTLE_HERO_FROM_DB = true;
#else
    public static bool LOAD_BATTLE_HERO_FROM_DB = true;
#endif


#if UNITY_EDITOR
    public static bool DEBUG_HP = false;
    public static bool DEBUG_BATTLE = false;
#else
	public static bool DEBUG_HP = false;
	public static bool DEBUG_BATTLE = false;
#endif


#if UNITY_EDITOR || UNITY_STANDALONE
    public static bool USE_ASSET_BUNDLE = true;
#else
	public static bool USE_ASSET_BUNDLE = true;
#endif

    public const int CANVAS_HEIGHT = 640;
    //
    public const int FRAME_RATE_HIGHT = 50;
    public const int FRAME_RATE_LOW = 30;
    public const int REQ_LEVEL_FOR_PURCHASE = 30;
    public const int FEE_SHARE_BATTLE = 5;
    //
    public static bool ALLOW_SELECT_LEVEL_CAMPAIGN_AUTO = false;

    //#if UNITY_EDITOR
    //	public const string CP = "fk";
    //#else
    //    public const string CP = "fk";
    //#endif


    //
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    public const string OS = "OSX";

    //test
    //public const string OS = "IOS";
    //public const string OS = "ANDROID";


#elif UNITY_EDITOR
    public const string OS = "ANDROID";

#elif UNITY_IOS
	public const string OS = "IOS";


#elif UNITY_ANDROID
	public const string OS = "ANDROID";

#elif UNITY_STANDALONE_WIN
	public const string OS = "PC";
#endif

    public const string VERSION_EN = "1.1.1";
    public const string VERSION_VN = "1.1.0";
    //
    public static string VERSION = DEFAULT_LANGUAGE == Language.VI ? VERSION_VN : VERSION_EN;//"1.1.0";
    //Build time se duoc update tu dong
    //spublic static string BUILD = BuildInfo.BUILD_TIME;
    //	public const string LANG = "VI";
    //	public static string LANG = "VI";
    //
    public static int daily_Time_Up = 0;

    //secods
    public const float CONNECTION_TIMEOUT = 15f;
    public const int NUMBER_GUILD_AVATAR = 10;

    //
    public const int REQUIRE_LEVEL_MARKET = 15;
    public const int REQUIRE_LEVEL_GUILD = 21;
    public const int REQUIRE_LEVEL_ASPEN_DUNGEON = 72;
    public const int REQUIRE_LEVEL_BRAVE_TRIAL = 40;
    public const int REQUIRE_LEVEL_SUPER_LUCKY_SPIN = 80;
    public const int REQUIRE_LEVEL_GOLD_CHALLENGE = 20;
    public const int REQUIRE_LEVEL_BRAVE_CHALLENGE = 25;
    public const int REQUIRE_LEVEL_HERO_CHALLENGE = 30;
    public const int REQUIRE_LEVEL_PET = 70;
    public const int REQUIRE_LEVEL_TARVEN = 28;
    public const int REQUIRE_LEVEL_CRYSTAL_CROWN = 14;
    public const int REQUIRE_LEVEL_TRIAL_CHAMPION = 55;
    public const int REQUIRE_LEVEL_ALL_STARS_LEAGUE = 80;
    public const int REQUIRE_LEVEL_TOWER = 24;
    public const int REQUIRE_LEVEL_ALTAR = 20;
    public const int REQUIRE_LEVEL_SEALED_LAND = 80;
    public const int REQUIRE_LEVEL_EVENT_RAID = 20;
    public const int REQUIRE_LEVEL_MARINEFORD = 65;

    //
    public const int REQUIRE_LEVEL_OPEN_ALTAR = 25;
    public const int REQUIRE_LEVEL_OPEN_FUSION = 20;
    //minh panda
    public const int REQUIRE_LEVEL_ISLAND = 30;
    public const int REQUIRE_LEVEL_BOSS_FRIEND = 36;
    public const int REQUIRE_VIP_SUPER_LUCKY_SPIN = 3;
    //level
    public const int REQUIRE_GUILD_LEVEL_FOR_MILL = 7;
    public const int REQUIRE_GUILD_LEVEL_FOR_WAR = 10;

    public const int REQUIRE_QUEST_SUMMON_ZORO = 5;
    public const int REQUIRE_QUEST_CHECKIN = 7;
    public const int REQUIRE_QUEST_SHOP = 9;
    public const int REQUIRE_QUEST_SUMMON_NAMI = 12;
    public const int REQUIRE_QUEST_MISSION = 13;
    public const int REQUIRE_QUEST_FORGE = 14;
    public const int REQUIRE_QUEST_SUMMON_USOP = 16;
    public const int REQUIRE_QUEST_ACHIEVEMENT = 17;
    public const int REQUIRE_QUEST_EVENT = 18;
    public const int REQUIRE_QUEST_SUMMON_SANJI = 22;
    public const int REQUIRE_QUEST_LUCKYSPIN = 23;
    //

    //
    public const int REQUIRE_OPEN_EVENT = 8;
}

public enum Language : int
{
    VI = 1,
    EN = 2,
}

public enum HeroGender : int
{
    MALE = 1,
    FEMALE = 2,
}

public enum HeroAnimationType : int
{
    SPINE = 0,
    UNITY = 1,
}

public enum GameDirection : int
{
    LEFT = 1,
    RIGHT = 2
}

public enum SkillNumber : int
{
    SKILL_1,
    SKILL_2,
    SKILL_PASSIVE,
}


public enum ImpactPower : int
{
    NORMAL_HIT = 1,
    STRONG_HIT = 2
}

public enum ImpactEffectType : int
{
    HP_EFFECT = 1,
    RAGE_EFFECT = 2
}

public enum HeroFaction : int
{
    UNKNOWN = 999,
    //
    ABYSS = 6,
    FOREST = 5,
    FORTRESS = 4,
    SHADOW = 3,
    LIGHT = 2,
    DARK = 1

}

public enum HeroVillage : int
{
    UNKNOWN = 999,
    //
    NONE=0,
    LEAF = 1,
    RAIN = 2,
    FOG = 3,
    WATERFALL = 4,
    STONE = 5,
    HOTWATER = 6,
    SAND = 7,
    SOUND = 8,
    CLOUD = 9,
}
public enum HeroClass : int
{
    UNKNOWN = 999,
    //
    MAGE = 2,
    WARRIOR = 5,
    PRIEST = 4,
    RANGER = 3,
    ASSASSIN = 1
}

public enum ModePlay : int
{
    NONE = 0,
    MODE_RAID = 1,
    MODE_CAMPAIGN = 2,
    MODE_TOWER_OBLIVION = 3,
    MODE_TOWER2 = 4,
    MODE_ASPEN_DUNGEON = 5,
    MODE_GUILD_BOSS = 6,
    MODE_FRIEND_BOSS = 7,
    MODE_BROKEN_SPACE = 8,
    MODE_BOSS_SERVER = 9,
    MODE_SEAL_LAND = 10,
    MODE_ISLAND_BOSS = 11,
    MODE_ISLAND_CREEP = 12,
    MODE_CRYSTALCROWN = 13,
    MODE_TRIALOFCHAMPION = 14,
    MODE_ALLSTARSLEAGUE = 15,
    MODE_PRACTICE = 16,
    MODE_CHALLENGE = 17,
    MODE_CLOUD_VILLAGE = 18,
    MODE_MAZE = 19
}



public enum EquipmentQuality : int
{
    UNKNOWN = 9999,
    //
    BLUE = 1,
    YELLOW = 2,
    PURPLE = 3,
    GREEN = 4,
    RED = 5,
    ORANGE = 6
}

public enum EquipmentType : int
{
    UNKNOWN = 9999,
    //
    WEAPON = 4,
    ARMOR = 3,
    SHOES = 2,
    ACCESSORY = 1
}

//public enum ShardType: int
//{
//	SHARD_UNKOWN_HERO = 1,
//	SHARD_ARTIFACT = 2,
//	SHARD_SPECIFIC_HERO = 3,
//}

public enum ArtifactType : int
{
    BLUE = 1,
    YELLOW = 2,
    PURPLE = 3,
    GREEN = 4,
    RED = 5,
    ORANGE = 6,
    NONE = 7
}

public enum LineupType : int
{
    CAMPAIGN = 1,
    FRIEND = 2,
    CELESTIAL = 3,
    GUILD_BOSS = 4,
    ARENA_CRYSTAL = 5,
    ARENA_TRIAL_CHAMPION = 6,
    ARENA_ALL_STARS_LEAGUE = 7,
}

public enum MissionStatus : int
{
    COMPLETED = 1,
    NOT_COMPLETED = 2,
    NOT_CLAIM_REWARD = 3
}

public enum ShopCurrencyType : int
{
    GEM = 1,
    GOLD = 2,
    //

}

public enum TweenEnum
{
    LocalPosition,
    Scale
}

public enum DropPositionType : int
{
    NONE = -1,
    CAMPAIGN = 1,
    MARKET = 2,
    CHALLENGE = 3,
    FORGE = 4,
    CASINO = 5,
    TAVERN = 6,
    BRAVE_TRIAL_STORE = 7,
    TRIAL_OF_CHAMPION_STORE = 8,
    PURCHASE = 9,
    DAILY_QUEST = 10,
    SUMMON_HERO = 11,
    GUILD = 12,
    CELESTIAL = 13,
    EVENT_RAID = 14,
    ALTAR = 15,
    GUILD_STORE = 16,
    CASINO_STORE = 17,
    ALTAR_STORE = 18,
    BRAVE_TRIAL = 19,
    TRIAL_OF_CHAMPION = 20,
    FRIEND = 21,
    TOWER_OF_OBLIVION = 22,
    ARENA = 23,
    BUY_COIN = 24,
    SUPER_CASINO = 25,
    HERO_LIST = 26,
    FUSE_HERO = 27,
    SCOUT = 28,
    MONTH_EVENT = 29,
    ENDOW_EVENT = 30,
    BUY_FOOD_ARMY = 31,
    BOSS_GUILD = 32,
    GUILD_SKILL = 33,
    FIGHT_CAMP = 34,
    WEEK_EVENT = 35,

    //
    SHOP_BIRD_FEATURE = 36,
    COOL_EVENT = 37,
}

public enum TYPE_AVATAR : int
{
    HERO = 0,
    SPECIAL = 1,
    SKIN = 2
}

public delegate void OnPublicDataCallback(object data);
public delegate void OnPublicMultilDataCallback(object[] data);
public delegate void OnPublicNoDataCallback();