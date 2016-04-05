using UnityEngine;
using System.Collections;

namespace Ip2
{
    public enum GameState { menu, game, scoreScreen};

    public class PersistentData : MonoBehaviour
    {
        public GameObject m_UIControls;

        int m_currentLevel;

        public int m_winningPlayer = -1;

        public int player1Score = 0;
        public int player2Score = 0;
        public int player3Score = 0;
        public int player4Score = 0;

        public GameState m_state;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

            m_UIControls = GameObject.Find("UI Updater");

            if (Application.loadedLevel == 0)
            {
                m_state = GameState.menu;
                m_currentLevel = 0;
            }
            if (Application.loadedLevel == 1)
            {
                m_state = GameState.game;
                m_currentLevel = 1;
            }
            if (Application.loadedLevel == 2)
            {
                m_state = GameState.scoreScreen;
                m_currentLevel = 2;
            }
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

            if (Application.loadedLevel != m_currentLevel)
            {
                if (Application.loadedLevel == 0)
                {
                    m_state = GameState.menu;
                    m_currentLevel = 0;
                }
                if (Application.loadedLevel == 1)
                {
                    m_state = GameState.game;
                    m_currentLevel = 1;
                }
                if (Application.loadedLevel == 2)
                {
                    m_state = GameState.scoreScreen;
                    m_currentLevel = 2;
                }
            }
        }

        public void ResetData()
        {
            m_winningPlayer = -1;
            player1Score = 0;
            player2Score = 0;
            player3Score = 0;
            player4Score = 0;
    }
    }
}
