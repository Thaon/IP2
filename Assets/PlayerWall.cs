using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class PlayerWall : MonoBehaviour
    {
        #region Member Variables

        //public variables
        [HideInInspector]
        public bool m_isWallJumping = false;

        public float m_wallStickTime = 0.25f; //how long the player can stick in the wall jump state
        public bool m_canRecover = true; //this determines whether the player is allowed to recover from a failed wall jump
        public float m_freeFromWallTime = 0.1f; //if this goes to 0 the player can jump as soon as he touches the wall, it's there if needed

        //using the "State Machine" pattern here, to better modularize the different wall associated behaviors
        public class WallRunState
        {
            public float m_wallRunTime = 1.5f; //duration of wall run (max time allowed on a wall)
            public float m_wallRunSpeed = 5f;
            public bool m_canRunForever = false; //this can be enabled to allow the player to run as much as he pleases on a wall
            public bool m_wallRunDecay = true; //this will slow the player while running on a wall if true
        }
        public class WallSlideState
        {
            public float m_wallSlideTime = 1.5f; //same as above, but for when the player is sliding down
            public float m_wallSlideSpeed = 5f;
            public bool m_canSlideForever = false;
            public bool m_wallSlideBuildup = true; //this will gradually increase the slide speed if true
        }

        public WallRunState m_wallRun;
        public WallSlideState m_wallSlide;

        public GameObject m_leftWallCHeck;
        public GameObject m_rightWallCHeck;

        //private variables
        private Player m_player;
        private float m_wallRunTimer;  //how long the player can wall run
        private float m_wallSlideTimer;  //how long the player can slide
        private float m_wallStickTimer;  //how long can the player stick to a wall in general
        private float m_wallIdleTimer;  //how long can the player stick to a wall when not moving
        private bool m_isFacingAWall = false;
        private bool m_isWallRunning;  //is the player wall running
        private bool m_isWallSliding;  //is the player sliding down
        private bool m_isStuckToWall;  //is the player stuck on a wall

        //run decay and slide buildup
        private float m_slowdown;  //slowdown when wall running
        private float m_slowdownSpeed;  //wall run speed when slowing
        private float m_speedup;  //speedup when wall sliding
        private float m_speedupSpeed;  //speeding up when wall sliding

        #endregion

        // Use this for initialization
        void Start()
        {
            m_player = GetComponent<Player>();
            m_wallRun = new WallRunState();
            m_wallSlide = new WallSlideState();
        }

        void FixedUpdate() //used for everything physics
        {
            if (m_isWallRunning) //we check if we can run forever, if not, the speed will decay
            {
                if (!m_wallRun.m_canRunForever && m_wallRun.m_wallRunDecay)
                {
                    m_player.SetYSpeed(m_slowdownSpeed);
                }
                else
                {
                    m_player.SetYSpeed(m_wallRun.m_wallRunSpeed);
                }
            }

            if (m_isWallSliding) //same here but for the incremental buildup of speed
            {
                if (m_wallSlide.m_wallSlideBuildup)
                {
                    m_player.SetYSpeed(-m_speedupSpeed);
                }
                else
                {
                    m_player.SetYSpeed(-m_wallSlide.m_wallSlideSpeed);
                }
            }

            if (m_isStuckToWall) //self explanatory
            {
                m_player.SetXSpeed(0);
            }

        }

        // Update is called once per frame
        void Update()
        {
            //we reset the states at each cycle
            m_isWallRunning = false;
            m_isWallSliding = false;
            m_isStuckToWall = false;

            //we then apply the delta of the speeds for wall running and sliding
            if (!m_wallRun.m_canRunForever && m_wallRun.m_wallRunDecay)
            {
                m_slowdownSpeed = (Time.deltaTime / m_wallRun.m_wallRunTime) * m_wallRun.m_wallRunSpeed;
            }
            if (m_wallSlide.m_wallSlideBuildup)
            {
                m_speedupSpeed = (Time.deltaTime / m_wallSlide.m_wallSlideTime) * m_wallSlide.m_wallSlideSpeed;
            }

            //we now check if we are hitting a wall
            if (!m_player.m_isOnGround)
            {
                // The player is facing a wall if a linecast between the 'wallCheck' objects hits anything on the wall layer. REVIEW!

                RaycastHit2D hitLWall = Physics2D.Linecast(m_player.transform.position, m_leftWallCHeck.transform.position, m_player.m_surfaces);
                RaycastHit2D hitRWall = Physics2D.Linecast(m_player.transform.position, m_rightWallCHeck.transform.position, m_player.m_surfaces);
                Debug.DrawLine(m_player.transform.position, m_leftWallCHeck.transform.position, Color.red);
                Debug.DrawLine(m_player.transform.position, m_rightWallCHeck.transform.position, Color.red);

                if (hitLWall || hitRWall)//if we are hitting any wall, we start wallrunning (if we are not falling)
                    m_isFacingAWall = true;

                if (!m_player.m_isFalling)
                {
                    if (m_isFacingAWall)
                    {
                        if ((m_player.m_facingDir && m_player.m_hAxis > 0) || (!m_player.m_facingDir && m_player.m_hAxis < 0)) //if the player is moving towards the wall, we stick to it
                        {
                            if (!m_player.m_isOnWall)
                            {
                                m_player.SetStuckToWall(true);
                                m_player.SetXSpeed(0);
                                Debug.Log("sticking");

                                //reset the timers
                                m_wallStickTimer = m_wallStickTime;
                                m_wallIdleTimer = m_freeFromWallTime;
                            }

                            WallRunning();

                            if (!m_isWallRunning)
                            {
                                WallSliding();
                            }
                            else //if not running or sliding
                            {
                                StartWallUnstickTimer();
                            }
                            // Or else if the player can wall jump...
                        }
                        else
                        {
                            StartWallUnstickTimer();
                        }
                    }

                    // Or else if the player can wall jump...
                }
                else if (m_player.m_isOnWall)
                {
                    WallJumping();
                }
                else
                {
                    if (m_player.m_isOnWall && m_player.m_hAxis == 0)
                    {
                        Fall();
                        Unstick();
                    }
                }
            }

            //player.SetWallAnimation(wallRunning, wallSliding, stuckToWall); //set animator values

            if(m_player.m_isOnGround)
            {
                //Unstick();
            }
        }

        void WallRunning()
        {
            // if we are still allowed to wall run, we do
            if (m_wallRun.m_canRunForever || (!m_wallRun.m_canRunForever && m_wallRunTimer > 0))
            {
                if (!m_wallRun.m_canRunForever)
                {
                    m_wallRunTimer -= Time.deltaTime;

                    if (m_wallRun.m_wallRunDecay)
                    {
                        m_slowdownSpeed -= m_slowdown; //we slow down if we can't run forever
                    }
                }
                m_isWallRunning = true;
            }
        }

        void WallSliding() //exactly as above, but for wall slide
        {
            if (m_wallSlide.m_canSlideForever || (!m_wallSlide.m_canSlideForever && m_wallSlideTimer > 0))
            {
                if (!m_wallSlide.m_canSlideForever)
                {
                    m_wallSlideTimer -= Time.deltaTime;
                }
                if (m_wallSlide.m_wallSlideBuildup && m_speedupSpeed < m_wallSlide.m_wallSlideSpeed)
                {
                    m_speedupSpeed += m_speedup;
                }
                m_isWallSliding = true;
            }
            else
            {
                StartWallUnstickTimer();
            }
        }

        void WallJumping()
        {
            // If in the air, stuck on the wall and moving in the opposite direction...
            if (!m_player.m_isOnGround && m_player.m_isOnWall && ((!m_player.m_facingDir && m_player.m_hAxis < 0) || (m_player.m_facingDir && m_player.m_hAxis > 0)))
            {
                // ... start the wallStickTimer.
                if (m_wallStickTimer > 0)
                {
                    m_wallStickTimer -= Time.deltaTime;
                    m_isStuckToWall = true;
                    Debug.Log(m_wallStickTime);

                    // When jumping while being stuck, make sure the jumps are reset and the player jumps.
                    if (Input.GetButtonDown("Jump"))
                    {
                        WallJump();
                    }
                }
                else
                {
                    // Make the player fall down when the timer is completed.
                    Unstick();
                    Fall();
                }
                // Or else make the player fall down (when not moving) and then unstick the player.
            }
            else
            {
                if (m_player.m_isOnWall && m_player.m_hAxis == 0)
                {
                    Fall();
                    Unstick();
                }
            }
        }

        void WallJump()
        {
            m_isStuckToWall = false;
            m_isWallRunning = false;
            m_isWallSliding = false;
            Unstick();
            m_isWallJumping = true;

            m_player.Jump();
        }

        void Unstick()
        {
            Debug.Log("unsticking");
            // Make the player no longer stuck to the wall.
            m_player.SetStuckToWall(false);
            m_isFacingAWall = false;

            // Reset wall running properties.
            if (!m_isWallRunning && !m_wallRun.m_canRunForever)
            {
                // Reset the runSlowdownSpeed.
                if (m_wallRun.m_wallRunDecay)
                    m_slowdownSpeed = m_wallRun.m_wallRunSpeed;

                // Reset the wallRunTimer.
                m_wallRunTimer = m_wallRun.m_wallRunTime;
            }

            // Reset wall sliding properties.
            if (!m_isWallSliding)
            {
                // Reset the slideSpeedupSpeed.
                if (m_wallSlide.m_wallSlideBuildup)
                    m_speedupSpeed = 0f;

                // Reset the wallSlideTimer.
                m_wallSlideTimer = m_wallSlide.m_wallSlideTime;
            }

            // Reset isWallJumping.
            m_isWallJumping = false;
        }

        void Fall()
        {
            if (m_canRecover)
            {
                // Make sure the player is falling.
                m_player.Fall();
            }
        }

        // Function to run the wallUnstickTimer.
        void StartWallUnstickTimer()
        {
            // When the player is not moving, make sure the player is unstuck when the timer is completed.
            if (m_wallIdleTimer > 0)
            {
                m_wallIdleTimer -= Time.deltaTime;
            }
            else
            {
                Unstick();
                Fall();
            }
        }

    }
}
