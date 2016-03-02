using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class Checkpoints : MonoBehaviour {



        // Use this for initialization
        void Start() {

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
                        if (data.player1Score > 4)
                        {
                            data.m_winningPlayer = 1;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 2:
                        data.player2Score++;
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 2;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 3:
                        data.player3Score++;
                        if (data.player2Score > 4)
                        {
                            data.m_winningPlayer = 3;
                            Application.LoadLevel("Endscene");
                        }
                    break;

                    case 4:
                        data.player4Score++;
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