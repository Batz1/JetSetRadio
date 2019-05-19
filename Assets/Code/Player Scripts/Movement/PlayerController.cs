using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    public GameObject playerSprite;
    public GroundCheck gCheck;

    public Collider2D wallBumpL;
    public Collider2D wallBumpR;

    Collider2D lastCol;

    [Header("Horizontal Speed")]
    public float walkingSpeed;
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

    [Header("Time To Remember Jump Input")]
    public float jumpPressTime = 0.2f;
    float jumpPressRemember;

    [Header("Dash Forces")]
    public float upForce;
    public float dashForce;

    float breakForce;

    [Range(0f, 5f)]
    public float minBreakForce;

    [Range(0f, 5f)]
    public float maxBreakForce;

    [Header("Wall jump speeds")]
    public float wallJumpSpeedX;
    public float wallJumpSpeedY;

    [Header("Jump Booleans")]
    public bool canJump = true;
    public bool canDoubleJump = false;

    [Header("Wall Jump Booleans")]
    public bool canWallJumpL = false;
    public bool canWallJumpR = false;
    bool isSameWall = false;

    [Header("Dash Booleans")]
    public bool canDashDown = false;
    public bool canDashLeft = true;
    public bool canDashRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xVelocityCheck = rb.velocity.x;
        yVelocityCheck = rb.velocity.y;

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

        if (Input.GetKeyDown(KeyCode.S))
        {
            jumpPressRemember = jumpPressTime;

            if (canDoubleJump)
            {
                DoubleJump();
                canJump = false;
                canDoubleJump = false;
            }
        }

        if (jumpPressRemember > 0 && canJump)
        {
            jumpPressRemember = 0;
            Jump();
            canJump = false;
            canDoubleJump = true;
            canDashDown = true;
        }


        // Horizontal movement
        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (rb.velocity.x > 0)
            {
                if (Input.GetAxis("Horizontal") <= 0f)
                {
                    Moving(Input.GetAxisRaw("Horizontal"));
                    hSpeed = turningSpeed;
                }
                else
                {
                    if (rb.velocity.x < speedLimit)
                    {
                        Moving(Input.GetAxisRaw("Horizontal"));
                    }

                    hSpeed = walkingSpeed;
                }
            }
            else if (rb.velocity.x < 0)
            {
                if (Input.GetAxis("Horizontal") >= 0f)
                {
                    Moving(Input.GetAxisRaw("Horizontal"));
                    hSpeed = turningSpeed;
                }
                else
                {
                    if (rb.velocity.x > -speedLimit)
                    {
                        Moving(Input.GetAxisRaw("Horizontal"));
                    }

                    hSpeed = walkingSpeed;
                }
            }
            else
            {
                Moving(Input.GetAxisRaw("Horizontal"));
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
            if (Input.GetKeyDown(KeyCode.A) && canDashLeft)
            {
                if (!canDashRight)
                {
                    Dash(1, 1.5f);
                }
                else
                {
                    Dash(1, 1);
                }
                canDashRight = true;
                canDashLeft = false;
            }
            else if (Input.GetKeyDown(KeyCode.D) && canDashRight)
            {
                if (!canDashLeft)
                {
                    Dash(-1, 1.5f);
                }
                else
                {
                    Dash(-1, 1);
                }
                canDashRight = false;
                canDashLeft = true;
            }
        }
 
        // Down Kick
        if (Input.GetKeyDown(KeyCode.W) && canDashDown)
        {
            DownKick();
            canDashDown = false;
        }

        // Wall Jumps
        if (Input.GetKeyDown(KeyCode.S) && canWallJumpL && !isSameWall)
        {
            WallJump(1);
            canWallJumpL = false;

            canDashRight = false;
            canDashLeft = true;
            canDashDown = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && canWallJumpR && !isSameWall)
        {
            WallJump(-1);
            canWallJumpR = false;

            canDashRight = true;
            canDashLeft = false;
            canDashDown = true;
        }
        
    }

    private void LateUpdate()
    {
        // Ground Check
        if (gCheck.isGrounded && !canDoubleJump)
        {
            canJump = true;
            canDoubleJump = false;

            isSameWall = false;
            lastCol = null;

            canDashLeft = true;
            canDashRight = true;
            canDashDown = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canJump)
        {
            if (collision.otherCollider == wallBumpL)
            {
                if (collision.collider != lastCol)
                {
                    isSameWall = false;
                }
                else
                {
                    isSameWall = true;
                }

                canDoubleJump = false;
                canWallJumpL = true;
                canWallJumpR = false;
            }
            else if (collision.otherCollider == wallBumpR)
            {
                if (collision.collider != lastCol)
                {
                    isSameWall = false;
                }
                else
                {
                    isSameWall = true;
                }

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
        if (collision.otherCollider == wallBumpL || collision.otherCollider == wallBumpR)
        {
            canWallJumpL = false;
            canWallJumpR = false;

            lastCol = collision.collider;
        }
    }

    public void Moving(float dir)
    {
        rb.AddForce(Vector2.right * dir * hSpeed);
        ///rb.velocity = new Vector2(hSpeed * dir, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
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
        rb.velocity = new Vector2(rb.velocity.x + (wallJumpSpeedX * dir), wallJumpSpeedY);
    }
}
