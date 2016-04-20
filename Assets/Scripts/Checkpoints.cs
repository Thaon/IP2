using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class Checkpoints : MonoBehaviour {

        public GameObject m_P1marker;
        public GameObject m_P2marker;
        public GameObject m_P3marker;
        public GameObject m_P4marker;

        bool p1CanCollect = true;
        bool p2CanCollect = true;
        bool p3CanCollect = true;
        bool p4CanCollect = true;

        CheckpointMG m_controller;

        void Start()
        {
            m_P1marker.SetActive(false);
            m_P2marker.SetActive(false);
            m_P3marker.SetActive(false);
            m_P4marker.SetActive(false);

            m_controller = FindObjectOfType(typeof(CheckpointMG)) as CheckpointMG;
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player")
            {
                PersistentData data = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>();
                switch (other.gameObject.GetComponent<Player>().m_uniqueID)
                {
                    case 0:
                        if (p1CanCollect)
                    {
                        m_P1marker.SetActive(true);
                        p1CanCollect = false;
                        m_controller.UpdateFlag(0);
                    }
                    break;

                    case 1:
                    if (p2CanCollect)
                    {
                        m_P2marker.SetActive(true);
                        p2CanCollect = false;
                        m_controller.UpdateFlag(1);
                    }
                    break;

                    case 2:
                    if (p3CanCollect)
                    {
                        m_P3marker.SetActive(true);
                        p3CanCollect = false;
                        m_controller.UpdateFlag(2);
                    }
                    break;

                    case 3:
                    if (p4CanCollect)
                    {
                        m_P4marker.SetActive(true);
                        p4CanCollect = false;
                        m_controller.UpdateFlag(3);
                    }
                    break;
                }
            }
        }
    }
}