using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class PlayerJump : MonoBehaviour
    {

        #region Member Variables

        //public variables
        public bool m_canJump = true;

        public bool m_pressingJumpBtn = false;

        public float m_jumpForce = 40f;

        //the following take care of jumping higher if the player holds the jump button
        public float m_JumpSpeed = 5f; //Y speed (not a force)
        public float m_jumpTime = 0.3f;
        public float m_addedJumpForce = 20f;
        public bool m_isJumping = false;
        private float m_airTime; //used to check how long the player can stay in the air

        //private variables
        private Player m_player;

        #endregion

        // Use this for initialization
        void Start()
        {
            m_player = GetComponent<Player>();
        }

        void FixedUpdate() //used for everything physics
        {
            //if (m_canJump)
            //{
            //    //set the Y speed.
            //    float ySpd = (m_JumpSpeed);
            //    //then we apply it to the player
            //    m_player.SetYSpeed(ySpd);

            //}
            //else
            if (m_pressingJumpBtn && m_isJumping) //if we are still jumping but we can go higher
            {
                if (m_airTime > 0)
                {
                    m_airTime -= Time.deltaTime;
                    //Debug.Log(m_airTime);

                    //finally we set the Y force
                    m_player.m_rigidbody.AddForce(new Vector2(0f, m_jumpForce));
                }
                else //we are done jumping
                {
                    m_isJumping = false;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            //we take care of jumping here
            if (m_canJump && m_pressingJumpBtn)
            {
                if (m_player.m_isOnGround) //check if working on walls
                {
                    //we start jumping
                    Jump();
                }
            }
            if (m_player.m_isOnGround)
            {
                //m_isJumping = false;
                m_canJump = true;
                m_airTime = m_jumpTime;
            }
        }

        public void Jump()
        {
            m_isJumping = true;
            m_player.m_isJumping = true;
            m_airTime = m_jumpTime;
            m_canJump = false;
        }

        public void ResetAirTime()
        {
            m_airTime = m_jumpTime;
        }
    }
}
