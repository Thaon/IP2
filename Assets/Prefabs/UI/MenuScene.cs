using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class MenuScene : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (!GameObject.Find("PersistentDataGO"))
            {
                GameObject data = new GameObject("PersistentDataGO");
                data.AddComponent<PersistentData>();
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GoToGameScene()
        {
            Application.LoadLevel(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}