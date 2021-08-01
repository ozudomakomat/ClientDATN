using pbdson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLog
{
    public class BattleLogSender : MonoBehaviour
    {
        public const int BATTLE_LOG_STATUS = 11000;
        public const int BATTLE_LOG_ATTACK = 11001;
        public const int BATTLE_LOG_INFO_AVG = 11002;
        public const int BATTLE_LOG_INFO_BATTLE = 11003;
        public const int BATTLE_LOG_INFO_BATTLE_DETAIL = 11004;
        public const int BATTLE_LOG_HISTORY = 11005;
        public const int BATTLE_TEST_ADD_GROUP = 11006;
        public const int BATTLE_TEST_REPLAY = 11007;
        public const int BATTLE_TOWER_LOG = 11008;
        public string urlServer = "http://103.9.207.52:7003";
        private NetworkController m_NetworkController;

        private static BattleLogSender m_Instance;
        public static BattleLogSender GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new BattleLogSender();
            }
            return m_Instance;
        }
        private void Awake()
        {
            m_NetworkController = NetworkController.GetInstance();
            Debug.Log("send sv : "+urlServer);
            m_NetworkController.m_UrlGameServer = urlServer;

        }
        public void RequestBattleLogStatus()
        {
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_STATUS);
        }
        public void ReplayBattle(long battleId)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(battleId);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_TEST_REPLAY, cv);
        }

        public void AddbattleGroup(List<long> userIds,string desc)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.AddRange(userIds);
            cv.aString.Add(desc);
            if (m_NetworkController == null)
            {  
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_TEST_ADD_GROUP, cv);
        }

        public void RequestBattleLogAttack(int userId, int groupId, int numberAtk)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(userId);
            cv.aLong.Add(groupId);
            cv.aLong.Add(numberAtk);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_ATTACK, cv);
        }
        public void RequestBattleTowerAttack(int tower,int userId, int level, int numberAtk)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(tower);
            cv.aLong.Add(userId);
            cv.aLong.Add(level);
            cv.aLong.Add(numberAtk);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_TOWER_LOG, cv);
        }

        public void RequestBattleLogInfoAvg(int avgId)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(avgId);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_INFO_AVG, cv);
        }

        public void RequestBattleLogInfoBattle(int avgId)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(avgId);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_INFO_BATTLE, cv);
        }
        public void RequestBattleLogInfoBattleDetail(long battleId)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(battleId);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_INFO_BATTLE_DETAIL, cv);
        }
        public void RequestBattleLogHistory(int battleId)
        {
            CommonVector cv = new CommonVector();
            cv.aLong.Add(battleId);
            if (m_NetworkController == null)
            {
                m_NetworkController = NetworkController.GetInstance();
            }
            m_NetworkController.m_UrlGameServer = urlServer;
            m_NetworkController.SendProtoPackage(BATTLE_LOG_HISTORY, cv);
        }
    }
}