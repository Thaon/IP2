using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class CheckGamepadStatus : MonoBehaviour
    {
        XInputTestCS m_inputHandler;
        public int m_playerNumber;
        public Sprite m_active, m_inactive;

        void Start()
        {
            m_inputHandler = FindObjectOfType(typeof(XInputTestCS)) as XInputTestCS;
        }

        void Update()
        {
            if (m_inputHandler.m_players[m_playerNumber].activeInHierarchy)
            {
                GetComponent<Image>().sprite = m_active;
            }
            else
            {
                GetComponent<Image>().sprite = m_inactive;
            }
        }
    }
}