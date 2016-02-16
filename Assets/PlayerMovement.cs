using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Member Variables

        //public variables
        public float m_moveSpeed = 5f;
        public float moveForce = 50f; //used for physics interactions

        //private variables
        private Player m_player;

        #endregion

        // Use this for initialization
        void Start()
        {
            m_player = GetComponent<Player>();
        }

        void FixedUpdate() //we use FixedUpdate for everything physics
        {
            if (!m_player.m_isOnWall) //we check that the player is not stuck against a wall first
            {
                //if the player is changing direction
                if (m_player.m_hAxis * m_player.m_rigidbody.velocity.x < m_moveSpeed)
                {
                    //add a force to the player.
                    //m_player.m_rigidbody.AddForce(transform.rotation * Vector2.right * m_player.m_hAxis * m_moveSpeed, ForceMode2D.Impulse); // * m_player.GetMovementForce(moveForce)
                    m_player.SetXSpeed(m_player.m_hAxis * m_moveSpeed);
                }
                // If the player's horizontal velocity is greater than the speed, then we set the player's velocity to the speed in the x axis.
                if (Mathf.Abs(m_player.m_rigidbody.velocity.x) > m_moveSpeed)
                {
                    m_player.SetXSpeed(Mathf.Sign(m_player.m_rigidbody.velocity.x) * m_moveSpeed);
                }

                //update the facing direction
                if (m_player.m_hAxis < 0)
                    m_player.m_facingDir = false; //we are going left
                else if (m_player.m_hAxis > 0)
                    m_player.m_facingDir = true; //we are going right
                else //if the Horizontal axis is 0, we stop
                {
                    m_player.SetXSpeed(0);
                }
            }
            else
            {
                Debug.Log("is on wall");
            }
        }
    }
}
