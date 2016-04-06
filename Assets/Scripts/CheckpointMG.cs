using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Ip2
{
    class CheckpointMG : MonoBehaviour
    {
        private Player m_player;
        private PersistentData m_pData;
        private GameState m_state;

        void Start()
        {
            
            m_pData = FindObjectOfType(typeof(PersistentData)) as PersistentData;
            m_state = m_pData.m_state;
        }

        void Update()
        {
            //just keep track of internal variables, don't show score
        }
    }
}
