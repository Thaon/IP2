using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ip2
{
    public class UpdateUiData : MonoBehaviour
    {
        public Text p1, p2, p3, p4;

        void Start()
        {
            GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_p1score = p1;
            GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_p2score = p2;
            GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_p3score = p3;
            GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_p4score = p4;
        }
    }
}
