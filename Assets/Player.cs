﻿using UnityEngine;
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

        //private variables
        private PlayerJump m_pJump;
        private PlayerWall m_pWall;
        private PlayerMovement m_pMove;
        
        private Animator m_animator;  //the player's animator.
        private float m_gravityScale;  //gravity's scale.
        private bool m_isDead = false;  //checks if the player is dead.

        //the following are used in conjunction with the Animator to swap between states
        private bool m_isWallSliding = false;
        private bool m_isWallRunning = false;
        private bool m_isWallJumping = false;

        #endregion

        // Use this for initialization
        void Start()
        {
            //getting the required data and GameObjects
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_groundTransform = transform.FindChild("GroundCheckGO");
            m_pJump = GetComponent<PlayerJump>();
            m_pWall = GetComponent<PlayerWall>();
            m_pMove = GetComponent<PlayerMovement>();
            m_animator = GetComponent<Animator>();
            m_gravityScale = m_rigidbody.gravityScale;

            // Check which direction the player is facing based on the spriteDirection and flip when the spriteDirection is Left.
            if (m_direction == Dir.e_right)
            {
                m_facingDir = true; //meaning, right
            }
            else
            {
                m_facingDir = false; //meaning left... so we switch it
                m_facingDir = !m_facingDir;

                //then we flip the sprite on the X axis multiplying the scale by -1
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        void FixedUpdate() //we are using FixedUpdate for all physics related
        {
            if (m_isOnWall) //self explanatory
            {
                m_rigidbody.gravityScale = 0;
            }
            else
            {
                m_rigidbody.gravityScale = m_gravityScale;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //we first get the input
            m_hAxis= Input.GetAxis("Horizontal");

            //then we set all the various animator values
            /*
            animator.SetBool("grounded", grounded);
            animator.SetBool("walking", walking);
            animator.SetBool("crouching", crouching);
            animator.SetBool("sliding", sliding);
            animator.SetBool("dashing", dashing);
            animator.SetBool("falling", falling);
            animator.SetBool("wall", stuckToWall);
            animator.SetBool("onLadder", onLadder);
            animator.SetBool("jumpingThrough", jumpingThrough);
            animator.SetFloat("horizontal", Mathf.Abs(hor));
            animator.SetFloat("xSpeed", Mathf.Abs(rigidbody.velocity.x));
            animator.SetFloat("ySpeed", rigidbody.velocity.y);
            */

            //we now check if the player is on the ground
            m_groundCheck = Physics2D.OverlapCircle(m_groundTransform.position, m_groundCheckRadius);

            if (m_groundCheck)
            {
                m_isOnGround = true;
                m_isJumping = false;
                m_isFalling = false;
            }
            else
            {
				m_isOnGround = false;
			}

            //we now chack if the player turned around and if so we mirror the sprite
            if ((m_hAxis > 0 && !m_facingDir) || (m_hAxis < 0 && m_facingDir))
            {
                //see above for explanation
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        //we now take care of movement

        public void SetXSpeed(float xSpd)
        {
            if (!m_isOnGround)
            {
                // Check if the player is wall jumping and reset the variable if the player is wall jumping.
                //bool isWallJumping = playerWall && playerWall.wallJump.enabled && playerWall.isWallJumping;
                //if (isWallJumping)
                    //playerWall.isWallJumping = false;
            }

            // Set XSpeed.
            m_rigidbody.velocity = new Vector2(xSpd, m_rigidbody.velocity.y);
        }

        public void SetYSpeed(float ySpd)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, ySpd);
        }

        public void Jump()
        {
            if (m_pJump)
            {
                m_pJump.Jump();
            }
        }

        public void Fall() //setter
        {
            m_isFalling = true;
            m_animator.SetTrigger("Falling");
        }

        public void OnWall(bool isOnWall) //setter
        {
            m_isOnWall = isOnWall;
        }

        //we now take care of the animator states for the wall jumping
        public void SetWallAnimation(bool running, bool sliding, bool jumping)
        {

            if (((m_isWallRunning && !running) || (m_isWallSliding && !sliding) || (m_isWallJumping && !jumping)) && !running && !sliding && !jumping)
            {
                m_animator.SetTrigger("actionsPerformed");
            }

            if (running && !m_isWallRunning)
            {
                m_animator.SetTrigger("WallRun");
            }

            if (sliding && !m_isWallSliding)
            {
                m_animator.SetTrigger("WallSlide");
            }

            if (jumping && !m_isWallJumping)
            {
                m_animator.SetTrigger("WallJump");
            }

            // used to keep track of the player's state.
            m_isWallRunning = running;
            m_isWallSliding = sliding;
            m_isWallJumping = jumping;

            m_animator.SetBool("wallRunning", running);
            m_animator.SetBool("wallSliding", sliding);
            m_animator.SetBool("wallJumping", jumping);
        }

        public void SetStuckToWall(bool value)
        {
            m_isOnWall = value;
        }
    }
}