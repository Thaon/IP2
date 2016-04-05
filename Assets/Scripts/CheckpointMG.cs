using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Ip2
{
    class CheckpointMG
    {
        public GameObject m_UIControls;

        private Player m_player;
        private PersistentData m_pData;
        private GameState m_state;

        void Start()
        {
            m_pData = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>();
            m_UIControls = GameObject.Find("UI Updater");
            m_state = m_pData.m_state;
        }

        void Update()
        {
            if (m_UIControls != null)
            {
                if (m_state == GameState.game)
                {
                    m_UIControls.GetComponent<UpdateUI>().showMain = true;
                    m_UIControls.GetComponent<UpdateUI>().countdownActive = true;
                    if (m_UIControls.GetComponent<UpdateUI>().roundCountdown <= 0.1f)
                    {
                        //set up a winner here
                        Application.LoadLevel(2);
                    }
                }
            }
        }
    }
}
