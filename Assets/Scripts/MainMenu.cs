using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class MainMenu : MonoBehaviour
    {
        void Start()
        {
            if (!GameObject.Find("PersistentDataGO"))
            {
                GameObject pData = new GameObject();
                Instantiate(pData);
                pData.name = "PersistentDataGO";
                pData.AddComponent<PersistentData>();
            }
        }

        void Update() {

        }
    }
}