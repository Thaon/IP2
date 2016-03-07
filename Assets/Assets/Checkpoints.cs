using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class Checkpoints : MonoBehaviour {

        public GameObject m_p1UI;
        public GameObject m_p2UI;
        public GameObject m_p3UI;
        public GameObject m_p4UI;

        Text m_p1text;
        Text m_p2text;
        Text m_p3text;
        Text m_p4text;


        // Use this for initialization
        void Start() {
            m_p1text = m_p1UI.GetComponent<Text>();
            m_p2text = m_p2UI.GetComponent<Text>();
            m_p3text = m_p3UI.GetComponent<Text>();
            m_p4text = m_p4UI.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update() {
            
        }
        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player")
            {
                PersistentData data = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>();
                switch (other.gameObject.GetComponent<Player>().m_uniqueID)
                {
                    case 1:
                        data.player1Score++;
                        m_p1text.text = data.player1Score.ToString();
                        if (data.player1Score > 4)
                        {
                            data.m_winningPlayer = 1;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 2:
                        data.player2Score++;
                        m_p2text.text = data.player2Score.ToString();
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 2;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 3:
                        data.player3Score++;
                        m_p3text.text = data.player3Score.ToString();
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 3;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 4:
                        data.player4Score++;
                        m_p4text.text = data.player4Score.ToString();
                        if (data.player4Score > 4)
                        {
                            data.m_winningPlayer = 4;
                            Application.LoadLevel("Endscene");
                        }
                    break;
                }
            }
        }
    }
}