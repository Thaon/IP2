using UnityEngine;
using System.Collections;

namespace Ip2
{
    [RequireComponent(typeof(BoxCollider2D))]

    public class KillPlayer : MonoBehaviour {

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().Respawn();
            }
        }
    }
}
