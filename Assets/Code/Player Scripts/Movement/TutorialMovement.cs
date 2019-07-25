using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    PlayerInput pInp;

    float jumpSpeed;
    float doubleJumpSpeed;
    float dashForce;
    float dashDownSpeed;

    bool canDJ;

    public bool toDash;
    public bool toJump;
    public bool toDoubleJump;
    public bool toStomp;

    public bool enableLock;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        pInp = GetComponentInChildren<PlayerInput>();
    }

    // Update is called once per frame
    void Start()
    {
        jumpSpeed = pc.jumpSpeed;
        doubleJumpSpeed = pc.doubleJumpSpeed;
        dashForce = pc.dashForce;
        dashDownSpeed = pc.dashDownSpeed;
    }

    void Update()
    {
        if (pc.canDoubleJump)
        {
            canDJ = true;
        }

        if (enableLock)
        {
            ImplementLock();
        }
    }

    void ImplementLock()
    {
        if (toJump)
        {

        }
        else if (toDoubleJump)
        {

        }
        else if (toDash)
        {

        }
        else if (toStomp)
        {

        }
        else
        {

        }
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

    public void DownKick()
    {
        rb.velocity = new Vector2(rb.velocity.x * (1 - (2.5f * Time.deltaTime)), -dashDownSpeed);
    }

    public void Dash(float dir, float multiply)
    {
        rb.AddForce(((Vector2.right * multiply) * dir) * dashForce);
    }
}
