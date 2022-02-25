using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        private Rigidbody2D playerRb;
        private Animator anim;

        public float currentLives;
        public float maxLives;
        public GameObject deathEffect;
        public bool isDead;

        public float currentCoins;


        public KeyCode sprintingHeld;
        public KeyCode jump;
        public KeyCode crouch;
        public float maxSpeed;
        public float timeTillMaxSpeed;
        public float sprintSpeed;
        public float jumpForce;
        public float maxJumpSpeed;
        public float holdForce;
        public float buttonHoldTime;
        public Transform groundCheck;
        public float groundCheckRadius;
        public LayerMask ground;
        public float maxFallSpeed;
        

        private float acceleration, currentSpeed, horizontalInput, runTime, jumpCountDown;

        public bool isFacingLeft, isJumping, isGrounded;

        private Vector2 facingLeft;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;

            playerRb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            jumpCountDown = buttonHoldTime;
            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
            MovementPressed();

            if (isGrounded)
            {
                anim.SetBool("Grounded", true);
            } else
            {
                anim.SetBool("Grounded", false);
            }
            
            if (Input.GetKeyDown(jump) && isGrounded)
            {
                isJumping = true;

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Bounce")
            {
                AudioManager.Instance.PlayBounceSFX();
            }
        }

        public virtual bool MovementPressed()
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FixedUpdate()
        {
            Movement();
            Jump();
        }

        protected virtual void Movement()
        {
            
            if (MovementPressed())
            {
                anim.SetBool("Moving", true);
                acceleration = maxSpeed / timeTillMaxSpeed;
                runTime += Time.deltaTime;
                currentSpeed = horizontalInput * acceleration * runTime;
                CheckDirection();
            }
            else
            {
                anim.SetBool("Moving", false);
                acceleration = 0;
                runTime = 0;
                currentSpeed = 0;
            }
            SpeedMultiplier();
            if(playerRb.velocity.y >= maxJumpSpeed)
            {
                playerRb.velocity = new Vector2(currentSpeed, maxJumpSpeed);
            } else
            {
                playerRb.velocity = new Vector2(currentSpeed, playerRb.velocity.y);
            }
            
        }

        public virtual bool SprintingHeld()
        {
            if (Input.GetKey(sprintingHeld))
            {
                return true;
            }
            else
                return false;
        }

        public virtual bool JumpHeld()
        {
            if (Input.GetKey(jump))
            {
                return true;
            }
            else
                return false;
        }

        public virtual void Jump()
        {
            if (isJumping)
            {
                if(isGrounded)
                {
                    AudioManager.Instance.PlayJumpSFX();
                    playerRb.AddForce(Vector2.up * jumpForce);
                    //playerRb.AddForce(new Vector2(0f, jumpForce));
                }
                AdditionalAir();
            } else
            {
                jumpCountDown = buttonHoldTime;
            }
            if(playerRb.velocity.y <= maxFallSpeed)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, maxFallSpeed);
            }
        }

        protected virtual void AdditionalAir()
        {
            if (JumpHeld())
            {
                jumpCountDown -= Time.deltaTime;
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    isJumping = false;
                }
                else
                {
                    playerRb.AddForce(Vector2.up * holdForce);
                    //playerRb.AddForce(new Vector2(0f, holdForce));
                }
            }
            else
                isJumping = false;
        }

        protected virtual void Flip()
        {
            if (isFacingLeft)
            {
                transform.localScale = facingLeft;
            }
            if (!isFacingLeft)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        protected virtual void CheckDirection()
        {
            if (currentSpeed > 0)
            {
                if (isFacingLeft)
                {
                    isFacingLeft = false;
                    Flip();
                }
                if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }
            }
            if (currentSpeed < 0)
            {
                if (!isFacingLeft)
                {
                    isFacingLeft = true;
                    Flip();
                }
                if (currentSpeed < -maxSpeed)
                {
                    currentSpeed = -maxSpeed;
                }
            }
        }

        protected virtual void SpeedMultiplier()
        {
            if (SprintingHeld())
            {
                currentSpeed *= sprintSpeed;
            }
        }
    }
}
