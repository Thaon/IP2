using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class PersistentData : MonoBehaviour
    {

        public int m_winningPlayer = -1;

        public int player1Score = 0;
        public int player2Score = 0;
        public int player3Score = 0;
        public int player4Score = 0;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResetData()
        {
            m_winningPlayer = -1;
        }
    }
}
