using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public Rigidbody2D rb;

    public GameObject playerSprite;
    public GroundCheck gCheck;
    public PlayerInput pInput;

    public Collider2D wallBumpL;
    public Collider2D wallBumpR;

    Collider2D lastCol;

    [Header("Horizontal Speed")]
    public float walkingSpeed;
    public float airSpeed;
    public float turningSpeed;
    float hSpeed;
    public float speedLimit;

    [Header("Velocity check")]
    public float xVelocityCheck;
    public float yVelocityCheck;

    [Header("Vertical Speed")]
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float dashDownSpeed;
    public float fallMultiplier;

    [Header("Time To Remember Jump Input")]
    public float jumpPressTime = 0.2f;
    public float jumpPressRemember;

    [Header("Dash Forces")]
    public float upForce;
    public float dashForce;

    float breakForce;

    [Range(0f, 10f)]
    public float minBreakForce;

    [Range(0f, 10f)]
    public float maxBreakForce;

    [Header("Wall jump speeds")]
    public float wallJumpSpeedX;
    public float wallJumpSpeedY;
    [Range(0f, 1f)]
    public float wallJumpRatio;
    [Header("Wall run variables")]
    public float wallRunSpeed;
    public bool canWallRun;
    public float wallRunCooldownTimer;
    float wallRunDuration;

    [Header("Jump Booleans")]
    public bool canJump = true;
    public bool canDoubleJump = false;
    [HideInInspector] public bool isDoubleJumping = false;
    public bool collidedWithAnything = false;
    [HideInInspector] public bool hasDoubleJumped = false;

    [Header("Nothing to see here")]
    public int howManyDeadEnemies;

    [Header("Wall Jump Booleans")]
    public bool canWallJumpL = false;
    public bool canWallJumpR = false;
    public bool isSameWall = false;

    [Header("Dash Booleans")]
    public bool canDashDown = false;
    [HideInInspector] public bool isDashingDown = false;
    [HideInInspector] public bool hasDashedDown = false;
    public bool canDashLeft = true;
    [HideInInspector] public bool isDashingL = false;
    public bool canDashRight = true;
    [HideInInspector] public bool isDashingR = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xVelocityCheck = rb.velocity.x;
        yVelocityCheck = rb.velocity.y;

        if (!collidedWithAnything)
        {
            gCheck.isGrounded = false;
        }

        if(wallRunDuration > 0)
        {
            wallRunDuration -= Time.deltaTime;
        }
        else
        {
            canWallRun = false;
        }

        if (GameManager.instance.isPlayerAlive)
        {
            // Sprite flip
            if (rb.velocity.x >= -0.000001)
            {
                playerSprite.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                playerSprite.transform.localScale = new Vector2(-1, 1);
            }

            // Jump and double jump
            if (jumpPressRemember >= -0.2)
            {
                jumpPressRemember -= Time.deltaTime;
            }

            if (Input.GetButtonDown(pInput.jumpButton) || Input.GetAxis(pInput.rVertical) <= -0.5f)
            {
                jumpPressRemember = jumpPressTime;

                if (canDoubleJump)
                {
                    DoubleJump();
                    canJump = false;
                    canDoubleJump = false;
                    hasDoubleJumped = true;

                    isDoubleJumping = true;

                    canWallRun = false;
                }
                else
                {
                    isDoubleJumping = false;
                }
            }
            else
            {
                isDoubleJumping = false;
            }

            if (jumpPressRemember > 0 && canJump)
            {
                jumpPressRemember = 0;
                Jump();
                canJump = false;
                canDashDown = true;
            }
            else if (!canJump && (Input.GetAxis(pInput.rVertical) > -0.25f) && !hasDoubleJumped)
            {
                canDoubleJump = true;
            }

            if(rb.velocity.y < 0)
            {
                Fall();
            }


            // Horizontal movement
            if (Input.GetAxis("Horizontal") != 0f)
            {
                if (rb.velocity.x > 0)
                {
                    if (Input.GetAxis("Horizontal") <= 0f)
                    {
                        Moving(Input.GetAxis("Horizontal"));
                        hSpeed = turningSpeed;
                    }
                    else
                    {
                        if (rb.velocity.x < speedLimit)
                        {
                            Moving(Input.GetAxis("Horizontal"));
                        }

                        if (!gCheck.isGrounded)
                        {
                            hSpeed = airSpeed;
                        }
                        else
                        {
                            hSpeed = walkingSpeed;
                        }
                    }
                }
                else if (rb.velocity.x < 0)
                {
                    if (Input.GetAxis("Horizontal") >= 0f)
                    {
                        Moving(Input.GetAxis("Horizontal"));
                        hSpeed = turningSpeed;
                    }
                    else
                    {
                        if (rb.velocity.x > -speedLimit)
                        {
                            Moving(Input.GetAxis("Horizontal"));
                        }

                        if (!gCheck.isGrounded)
                        {
                            hSpeed = airSpeed;
                        }
                        else
                        {
                            hSpeed = walkingSpeed;
                        }
                    }
                }
                else
                {
                    Moving(Input.GetAxis("Horizontal"));
                    hSpeed = walkingSpeed;
                }

                if (rb.velocity.x > speedLimit || rb.velocity.x < -speedLimit)
                {
                    if (!gCheck.isGrounded)
                    {
                        gCheck.groundDetectRemember = gCheck.groundDetectTimer;
                    }

                    if (gCheck.cancelMomentum)
                    {
                        GroundBreak();
                    }

                    breakForce = minBreakForce;
                }
                else
                {
                    gCheck.groundDetectRemember = 0;
                    breakForce = maxBreakForce;
                }
            }
            else if (gCheck.cancelMomentum)
            {
                // Breaking momentum while grounded
                GroundBreak();
            }
            else
            {
                breakForce = minBreakForce;
            }


            // Dashing
            if (!gCheck.isGrounded)
            {
                if ((((Input.GetAxisRaw("Horizontal") < 0f || (Input.GetAxisRaw("Horizontal") == 0f && rb.velocity.x < 0)) && Input.GetButtonDown(pInput.dashButton)) || Input.GetAxis(pInput.rHorizontal) >= 0.5f || Input.GetKeyDown(KeyCode.D)) && canDashLeft)
                {
                    isDashingL = true;
                    isDashingR = false;

                    if (!canDashRight)
                    {
                        Dash(-1, 1.5f);
                    }
                    else
                    {
                        Dash(-1, 1);
                    }
                    canDashRight = true;
                    canDashLeft = false;

                    canWallRun = true;
                    wallRunDuration = wallRunCooldownTimer;
                }
                else if ((((Input.GetAxisRaw("Horizontal") > 0f || (Input.GetAxisRaw("Horizontal") == 0f && rb.velocity.x > 0)) && Input.GetButtonDown(pInput.dashButton)) || Input.GetAxis(pInput.rHorizontal) <= -0.5f || Input.GetKeyDown(KeyCode.A)) && canDashRight)
                {
                    isDashingR = true;
                    isDashingL = false;

                    if (!canDashLeft)
                    {
                        Dash(1, 1.5f);
                    }
                    else
                    {
                        Dash(1, 1);
                    }
                    canDashRight = false;
                    canDashLeft = true;

                    canWallRun = true;
                    wallRunDuration = wallRunCooldownTimer;
                }
                else
                {
                    isDashingL = false;
                    isDashingR = false;
                }
            }
            else
            {
                isDashingL = false;
                isDashingR = false;
            }


            // Down Kick
            if ((Input.GetAxis(pInput.rVertical) >= 0.5f || Input.GetButtonDown(pInput.stompButton)) && canDashDown)
            {
                DownKick();
                canDashDown = false;
                hasDashedDown = true;

                canWallRun = false;

                isDashingDown = true;
            }
            else
            {
                isDashingDown = false;
            }

            // Wall Jumps
            if ((Input.GetButtonDown(pInput.jumpButton) || Input.GetAxis(pInput.rVertical) <= -0.5f) && canWallJumpL && !isSameWall)
            {
                canWallJumpL = false;
                WallJump(1);

                canDashRight = true;
                canDashLeft = true;
                canDashDown = true;

                hasDoubleJumped = true;
            }

            if ((Input.GetButtonDown(pInput.jumpButton) || Input.GetAxis(pInput.rVertical) <= -0.5f) && canWallJumpR && !isSameWall)
            {
                canWallJumpR = false;
                WallJump(-1);

                canDashRight = true;
                canDashLeft = true;
                canDashDown = true;

                hasDoubleJumped = true;
            }

        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }
    }

    private void LateUpdate()
    {
        // Ground Check
        if (gCheck.isGrounded)
        {
            canJump = true;
            canDoubleJump = false;
            hasDoubleJumped = false;

            isSameWall = false;
            lastCol = null;

            canDashLeft = true;
            canDashRight = true;
            canDashDown = false;

            hasDashedDown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canJump)
        {
            if ((collision.otherCollider == wallBumpL && canWallRun) || (collision.otherCollider == wallBumpR && canWallRun))
            {
                WallRun();
            }
            else if(collision.otherCollider == wallBumpL || collision.otherCollider == wallBumpR)
            {
                WallStop();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collidedWithAnything = true;

        if (!canJump)
        {
            if (collision.otherCollider == wallBumpL)
            {
                canDoubleJump = false;
                canWallJumpL = true;
                canWallJumpR = false;
            }
            else if (collision.otherCollider == wallBumpR)
            {
                canDoubleJump = false;
                canWallJumpR = true;
                canWallJumpL = false;
            }
        }
        else
        {
            canWallJumpR = false;
            canWallJumpL = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collidedWithAnything = false;

        if (collision.otherCollider == wallBumpL || collision.otherCollider == wallBumpR)
        {
            canWallJumpL = false;
            canWallJumpR = false;

            lastCol = collision.collider;
            canDashRight = true;
            canDashLeft = true;
            canDashDown = true;
        }

        if (canJump)
        {
            canJump = false;
            canDashDown = true;
        }

        if (!canJump && (Input.GetAxis(pInput.rVertical) > -0.25f) && !hasDoubleJumped)
        {
            canDoubleJump = true;
        }
    }

    public void Moving(float dir)
    {
        rb.AddForce(Vector2.right * dir * hSpeed);
        ///rb.velocity = new Vector2(hSpeed * dir, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpSpeed;
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * doubleJumpSpeed;
    }

    public void Fall()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    public void DownKick()
    {
        rb.velocity = new Vector2(rb.velocity.x * (1 - (2.5f * Time.deltaTime)), -dashDownSpeed);
    }

    public void Dash(float dir, float multiply)
    {
        rb.AddForce(((Vector2.right * multiply) * dir) * dashForce);
    }

    public void GroundBreak()
    {
        rb.velocity = new Vector2(rb.velocity.x * (1 - (breakForce * Time.deltaTime)), rb.velocity.y);
    }

    public void WallJump(float dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x + (wallJumpSpeedX * dir), wallJumpSpeedY);
    }

    public void WallStop()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * wallJumpRatio);
    }

    public void WallRun()
    {
        rb.velocity = new Vector2(rb.velocity.x, wallRunSpeed);
    }

}
