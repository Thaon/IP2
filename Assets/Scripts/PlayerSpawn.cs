using UnityEngine;
using System.Collections;

namespace Ip2
{
    class PlayerSpawn : MonoBehaviour
    {

        public GameObject m_player;

        public void Spawn()
        {
            m_player.transform.position = transform.position + new Vector3(0, 0, 1);
            m_player.SetActive(true);
        }


        public void DeSpawn()
        {
            m_player.transform.position = transform.position;
            m_player.SetActive(false);
        }
    }
}
