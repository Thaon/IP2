using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ip2
{
    public class CollectableMG : MonoBehaviour
    {
        public int m_numberOfCoins = 0;
        public PersistentData m_pData;

        void Start()
        {
            m_numberOfCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
            m_pData = FindObjectOfType(typeof(PersistentData)) as PersistentData;
        }

        public void UpdateCoins() //updated from CoinMG, will keep track of the amount of coins in the level, if 0 it will declare a winner
        {
            //play sound effect!!!
            m_numberOfCoins--;
            if (m_numberOfCoins <= 0)
            {
                int topScore = 0;
                int tempPlayer = 0;
                foreach(CoinMG coinCollector in FindObjectsOfType(typeof(CoinMG)))
                {
                    if (coinCollector.m_score > topScore)
                    {
                        tempPlayer = coinCollector.m_playerID;
                        topScore = coinCollector.m_score;
                    }
                }
                GameObject.Find("UI Updater").GetComponent<UpdateUI>().roundWinnerPlayerNumber = tempPlayer;

                m_pData.SetDictator(tempPlayer - 1);
                m_pData.m_state = GameState.roundFinished;

                //update global player scores
                switch (tempPlayer - 1) //temp player is 1 based, our case statement is 0 based
                {
                    case 0:
                        m_pData.player1Score++;
                        if (m_pData.player1Score >= m_pData.m_scoreToWin)
                        {
                            m_pData.m_state = GameState.scoreScreen;
                            Application.LoadLevel("EndScene");
                        }
                    break;

                    case 1:
                        m_pData.player2Score++;
                        if (m_pData.player2Score >= m_pData.m_scoreToWin)
                        {
                            m_pData.m_state = GameState.scoreScreen;
                            Application.LoadLevel("EndScene");
                        }
                    break;

                    case 2:
                        m_pData.player3Score++;
                        if (m_pData.player2Score >= m_pData.m_scoreToWin)
                        {
                            m_pData.m_state = GameState.scoreScreen;
                            Application.LoadLevel("EndScene");
                        }
                    break;

                    case 3:
                        m_pData.player4Score++;
                        if (m_pData.player3Score >= m_pData.m_scoreToWin)
                        {
                            m_pData.m_state = GameState.scoreScreen;
                            Application.LoadLevel("EndScene");
                        }
                    break;
                }
            }
        }

    }
}
