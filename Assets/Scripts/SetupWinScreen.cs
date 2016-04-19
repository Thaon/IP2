using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ip2
{
    public class SetupWinScreen : MonoBehaviour
    {
        PersistentData m_data;
        // Use this for initialization
        void Start()
        {
            m_data = GameObject.Find("PersistentDataGO").GetComponent<PersistentData>();

            Text winTxt = GameObject.Find("Winning Player: Player Number Text").GetComponent<Text>();

            switch (m_data.m_winningPlayer)
            {
                case 1:
                    winTxt.text = "PLAYER 1";
                    break;

                case 2:
                    winTxt.text = "PLAYER 2";
                    break;

                case 3:
                    winTxt.text = "PLAYER 3";
                    break;

                case 4:
                    winTxt.text = "PLAYER 4";
                    break;

                default:
                    winTxt.text = "NOBODY";
                    break;
            }

        }

        // Update is called once per frame
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        //reset the persistent variables and go to main menu
        //        m_data.ResetData();
        //        Application.LoadLevel(0);
        //    }
        //}
    }
}
