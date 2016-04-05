using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class Checkpoints : MonoBehaviour {

        public GameObject m_UI;
        bool p1CanCollect = true;
        bool p2CanCollect = true;
        bool p3CanCollect = true;
        bool p4CanCollect = true;

        void Start()
        {
            m_UI = GameObject.Find("UI Updater");
        }

        void Update()
        {
            
        }
        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player")
            {
                PersistentData data = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>();
                switch (other.gameObject.GetComponent<Player>().m_uniqueID)
                {
                    case 1:
                        if (p1CanCollect)
                    {
                        data.player1Score++;
                        m_UI.GetComponent<UpdateUI>().player1Score = data.player1Score;
                        if (data.player1Score > 4)
                        {
                            data.m_winningPlayer = 1;
                            Application.LoadLevel("Endscene");
                        }
                        p1CanCollect = false;
                    }
                    break;

                    case 2:
                    if (p2CanCollect)
                    {
                        data.player2Score++;
                        m_UI.GetComponent<UpdateUI>().player2Score = data.player2Score;
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 2;
                            Application.LoadLevel("Endscene");
                        }
                        p2CanCollect = false;
                    }
                    break;

                    case 3:
                    if (p3CanCollect)
                    {
                        data.player3Score++;
                        m_UI.GetComponent<UpdateUI>().player3Score = data.player3Score;
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 3;
                            Application.LoadLevel("Endscene");
                        }
                        p3CanCollect = false;
                    }
                    break;

                    case 4:
                    if (p4CanCollect)
                    {
                        data.player4Score++;
                        m_UI.GetComponent<UpdateUI>().player4Score = data.player4Score;
                        if (data.player4Score > 4)
                        {
                            data.m_winningPlayer = 4;
                            Application.LoadLevel("Endscene");
                        }
                        p4CanCollect = false;
                    }
                    break;
                }
            }
        }
    }
}