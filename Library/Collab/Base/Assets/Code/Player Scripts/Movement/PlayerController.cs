using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer sr;
    public GroundCheck gCheck;

    [Header("Horizontal Speed")]
    public float hSpeed;

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
    [Range(0.5f, 1f)]
    public float breakForce;

    [Header("Booleans")]
    public bool canJump = true;
    public bool canShortHop = true;
    public bool canDoubleJump = false;

    bool canDashDown = false;
    bool canDashLeft = true;
    bool canDashRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /// Sprite flip
        if(rb.velocity.x >= 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        /// Jump and double jump
        if (jumpPressRemember >= -0.2)
        {
            jumpPressRemember -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canShortHop)
        {
            ShortHop();
            canShortHop = false;
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

        /// Horizontal movement
        if (gCheck.isGrounded && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Moving(Input.GetAxisRaw("Horizontal"));
            gCheck.groundDetectRemember = gCheck.groundDetectTimer * 0.25f;
        }
        else if (gCheck.cancelMomentum)
        {
            /// Breaking momentum while grounded
            GroundBreak();
        }

        /// Dashing
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

        /// Down Kick
        if (Input.GetKeyDown(KeyCode.W) && canDashDown)
        {
            DownKick();
            canDashDown = false;
        }
        
    }

    private void LateUpdate()
    {
        /// Ground Check
        if (gCheck.isGrounded && !canDoubleJump)
        {
            canJump = true;
            canDoubleJump = false;
            canDashLeft = true;
            canDashRight = true;
            canDashDown = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canDashLeft = true;
        canDashRight = true;
    }

    public void Moving(float dir)
    {
        rb.velocity = new Vector2(hSpeed * dir, rb.velocity.y);
    }

    public void Jump()
    {
        //rb.AddForce(Vector2.up * upForce);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    public void ShortHop()
    {
        //rb.AddForce(Vector2.up * upForce);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * 0.5f);
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
    }

    public void DownKick()
    {
        rb.velocity = new Vector2(rb.velocity.x, -dashDownSpeed);
    }

    public void Dash(float dir, float multiply)
    {
        rb.AddForce(((Vector2.right * multiply) * dir) * dashForce);
    }

    public void GroundBreak()
    {
        rb.velocity = new Vector2(rb.velocity.x * breakForce, rb.velocity.y);
    }
}
