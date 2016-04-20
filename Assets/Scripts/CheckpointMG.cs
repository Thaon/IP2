using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Ip2
{
    class CheckpointMG : MonoBehaviour
    {
        private PersistentData m_pData;
        int m_flags;
        int[] m_pscore;

        void Start()
        {
            m_pData = FindObjectOfType(typeof(PersistentData)) as PersistentData;
            m_flags = FindObjectsOfType(typeof(Checkpoints)).Length;
            m_pscore = new int[4];
        }

        public void UpdateFlag(int player)
        { 
            m_pscore[player] ++;
            if (m_pscore[player] >= m_flags)
            {
                DeclareWinner();
            }
        }

        public void DeclareWinner()
        {
            int winner = 0;
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                if (m_pscore[i] > max)
                {
                    max = m_pscore[i];
                    winner = i;
                }
            }
            m_pData.SetDictator(winner);
            m_pData.m_state = GameState.roundFinished;
            m_pData.m_winningPlayer = winner + 1;

            //update global player scores
            switch (winner) //temp player is 1 based, our case statement is 0 based
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
                if (m_pData.player3Score >= m_pData.m_scoreToWin)
                {
                    m_pData.m_state = GameState.scoreScreen;
                    Application.LoadLevel("EndScene");
                }
                break;

                case 3:
                m_pData.player4Score++;
                if (m_pData.player4Score >= m_pData.m_scoreToWin)
                {
                    m_pData.m_state = GameState.scoreScreen;
                    Application.LoadLevel("EndScene");
                }
                break;
            }
        }
    }
}
