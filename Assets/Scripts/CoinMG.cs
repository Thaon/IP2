using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Ip2
{
    public class CoinMG : MonoBehaviour
    {
        public int m_score;
        public int m_playerID;

        public AudioClip m_pickup;

        private CollectableMG m_collMG;

        void Start()
        {
            m_collMG = FindObjectOfType(typeof(CollectableMG)) as CollectableMG;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //print("colliding");
            if (other.tag == "Coin")
            {
                //notify CollectableMG and make the coin disappear 
                m_score++;
                m_collMG.UpdateCoins();
                GetComponent<AudioSource>().PlayOneShot(m_pickup);
                other.gameObject.SetActive(false); //not using Destroy cause it would invoke the garbage collector
            }
        }
    }
}
