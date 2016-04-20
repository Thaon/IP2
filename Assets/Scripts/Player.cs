using UnityEngine;
using System.Collections;

namespace Ip2
{
    //this will be shared among the other classes
    public enum Dir { e_left, e_right}

    /// <summary>
    /// The Player class will take care of assembling the other classes (E.G. the PlayerMovement) into a single one that will communicate with the animator
    /// </summary>

    public class Player : MonoBehaviour
    {
        #region Member Variables

        //the following variables are kept hidden not to cludder the Editor with unnecessarily exposed variables
        [HideInInspector]
        public float m_hAxis;  //horizontal axis input.
        [HideInInspector]
        public bool m_facingDir;  //facing direction of the player. true is Right, false is Left
        [HideInInspector]
        public bool m_isOnGround = true;  //if the player is on the ground.
        [HideInInspector]
        public bool m_isOnWall = false;  //check if the player is on a wall.
        [HideInInspector]
        public bool m_isFalling = false;  //check if the player is falling.
        [HideInInspector]
        public Transform m_groundTransform;  //Transform of the ground check.
        [HideInInspector]
        public Collider2D m_groundCheck;  //the ground check collider.
        [HideInInspector]
        public bool m_isJumping = false;  //is the player jumping?
        [HideInInspector]
        public bool m_isJumpingThrough = false;  //if so, is he jumping through a platform?.
        [HideInInspector]
        public Rigidbody2D m_rigidbody;  //holds a reference to the player's rigidbody.

        //the following are required to tweak the behavior of the Player
        public Dir m_direction;
        public float m_groundCheckRadius = 0.12f;

        //these are the collision layers
        public LayerMask m_surfaces;

        //private variables
        private PlayerJump m_pJump;
        private PlayerWall m_pWall;
        
        private Animator m_animator;  //the player's animator. IMPORTANT: replace this with the spine runtime as soon as possible
        private float m_gravityScale;  //gravity's scale.
        private bool m_isDead = false;  //checks if the player is dead.

        //the following are used in conjunction with the Animator to swap between states
        private bool m_isWallSliding = false;
        //private bool m_isWallRunning = false; REMOVED FOR NOW
        private bool m_isWallJumping = false;

        private GameObject m_respawn;
        public bool m_hasBeenKilledOnce = false;

        public bool m_isTheDictator;

        public int m_uniqueID;

        public AudioClip m_jump;

        #endregion

        // Use this for initialization
        void Start()
        {
            if (GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_dictator == m_uniqueID)
            {
                m_isTheDictator = true;
            }
            else
            {
                m_isTheDictator = false;
            }

            if (!m_isTheDictator)
            {
                //generate the respawn object
                m_respawn = new GameObject("RespawnGO");
                m_respawn.transform.position = transform.position;

                //getting the required data and GameObjects
                m_rigidbody = GetComponent<Rigidbody2D>();
                m_groundTransform = transform.FindChild("GroundCheckGO");
                m_pJump = GetComponent<PlayerJump>();
                m_pWall = GetComponent<PlayerWall>();
                m_animator = transform.FindChild("SpineSpriteGO").GetComponent<Animator>();
                m_gravityScale = m_rigidbody.gravityScale;

                m_surfaces = LayerMask.GetMask("surfaces");

                // Check which direction the player is facing based on the spriteDirection and flip when the spriteDirection is Left.
                if (m_direction == Dir.e_right)
                {
                    m_facingDir = true; //meaning, right
                }
                else
                {
                    m_facingDir = false; //meaning left... so we switch it
                                         //m_facingDir = !m_facingDir;

                    //then we flip the sprite on the X axis multiplying the scale by -1
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                //GetComponent<Rigidbody2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerJump>().enabled = false;
                GetComponent<PlayerWall>().enabled = false;

                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    if (child.gameObject != this.gameObject)
                        child.gameObject.SetActive(false);
                }
            }
        }

        void FixedUpdate() //we are using FixedUpdate for all physics related
        {
            if (!m_isTheDictator)
            {
                if (m_isOnWall) //self explanatory
                {
                    m_rigidbody.gravityScale = 0;
                    Debug.Log("gravity is 0");
                }
                else
                {
                    m_rigidbody.gravityScale = m_gravityScale;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_isTheDictator)
            {
                //we first get the input
                //m_hAxis= Input.GetAxis("Horizontal"); replacing this with the new input manager

                //then we set all the various animator values
                if (m_hAxis != 0)
                {
                    m_animator.SetBool("moving", true);
                    if (m_hAxis < 0 && m_facingDir)
                    {
                        m_facingDir = false;
                        Vector3 scale = transform.localScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                    else if (m_hAxis > 0 && !m_facingDir)
                    {
                        m_facingDir = true;
                        Vector3 scale = transform.localScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                    //we now chack if the player turned around and if so we mirror the sprite
                    //if ((m_hAxis > 0 && !m_facingDir) || (m_hAxis < 0))
                    //{
                    //    //see above for explanation
                    //    Vector3 scale = transform.localScale;
                    //    scale.x *= -1;
                    //    transform.localScale = scale;
                    //}
                }
                else
                {
                    m_animator.SetBool("moving", false);
                }

                m_animator.SetBool("jumping", m_isJumping);
                m_animator.SetBool("falling", m_isFalling);
                m_animator.SetBool("wallSliding", m_isOnWall);
                m_animator.SetBool("onGround", m_isOnGround);


                //we now check if the player is on the ground
                m_groundCheck = Physics2D.OverlapCircle(m_groundTransform.position, m_groundCheckRadius, m_surfaces);

                //we then detect if he's falling
                if (m_rigidbody.velocity.y < 0)
                {
                    m_isFalling = true;
                }
                else
                {
                    m_isFalling = false;
                }

                if (m_groundCheck)
                {
                    m_isOnGround = true;
                    m_isJumping = false;
                    m_isFalling = false;
                    m_animator.SetBool("jumping", false);
                }
                else
                {
                    m_isOnGround = false;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                //GetComponent<Rigidbody2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerJump>().enabled = false;
                GetComponent<PlayerWall>().enabled = false;

                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    if (child.gameObject != this.gameObject)
                        child.gameObject.SetActive(false);
                }
            }
        }

        //we now take care of movement
        public void SetXSpeed(float xSpd)
        {
            if (!m_isOnGround)
            {
                // Check if the player is wall jumping and reset the variable if the player is wall jumping.
                bool m_isWallJumping = m_pWall.m_isWallJumping;
                if (m_isWallJumping)
                    m_pWall.m_isWallJumping = false;

                if (m_isOnWall)
                {
                    //m_rigidbody.velocity = new Vector2(0,0);
                    Debug.Log("on wall");
                }
                else
                {
                    m_rigidbody.velocity = new Vector2(xSpd, m_rigidbody.velocity.y);
                }
            }
            else
            {
                // Set XSpeed.
                m_rigidbody.velocity = new Vector2(xSpd, m_rigidbody.velocity.y);
            }
        }

        public void SetYSpeed(float ySpd)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, ySpd);
        }

        public void Jump()
        {
            if (m_pJump.m_pressingJumpBtn)
            {
                GetComponent<AudioSource>().PlayOneShot(m_jump);
                m_pJump.Jump();
            }
        }

        public void Fall() //setter
        {
            m_isFalling = true;
            //m_animator.SetTrigger("Falling");
        }

        //we now take care of the animator states for the wall jumping
        public void SetWallAnimation(bool running, bool sliding, bool jumping)
        {

            if (!running || !sliding || !jumping) //double check this
            {
                m_animator.SetTrigger("actionsPerformed");
            }
            /*
                if (running && !m_isWallRunning)
                {
                    m_animator.SetTrigger("WallRun");
                }
            */
            if (sliding && !m_isWallSliding)
            {
                m_animator.SetTrigger("WallSlide");
            }

            if (jumping && !m_isWallJumping)
            {
                m_animator.SetTrigger("WallJump");
            }

            // used to keep track of the player's state.
            //m_isWallRunning = running;
            m_isWallSliding = sliding;
            m_isWallJumping = jumping;

            //m_animator.SetBool("wallRunning", running);
            m_animator.SetBool("wallSliding", sliding);
            m_animator.SetBool("wallJumping", jumping);
        }

        public void SetStuckToWall(bool value)
        {
            m_isOnWall = value;
        }

        public void ResetJumpTimer()
        {
            m_pJump.ResetAirTime();
        }

        public void Respawn()
        {
            transform.position = m_respawn.transform.position;
            if (!m_hasBeenKilledOnce)
            {
                switch(GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().m_dictator)
                {
                    case 0:
                    GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player1Score++;
                    break;
                    case 1:
                    GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player2Score++;
                    break;
                    case 2:
                    GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player3Score++;
                    break;
                    case 3:
                    GameObject.Find("PersistentDataGO").GetComponent<PersistentData>().player4Score++;
                    break;
                }
                m_hasBeenKilledOnce = true;
            }
        }
    }
}