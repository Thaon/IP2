using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class GetRoundWinner : MonoBehaviour
    {
        public Text m_winningPlayer;

        public Text m_p1Score;
        public Text m_p2Score;
        public Text m_p3Score;
        public Text m_p4Score;

        void Start()
        {
            m_winningPlayer.text = "PLAYER " + (GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().GetWinningPlayerNumber() + 1).ToString();
            m_p1Score.text = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player1Score.ToString();
            m_p2Score.text = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player2Score.ToString();
            m_p3Score.text = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player3Score.ToString();
            m_p4Score.text = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player4Score.ToString();
        }
    }
}
