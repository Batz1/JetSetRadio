using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    public PlayerController pc;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("xVelocity", pc.xVelocityCheck);
        anim.SetFloat("yVelocity", pc.yVelocityCheck);
        anim.SetBool("isGrounded", pc.gCheck.isGrounded);

        if (!GameManager.instance.isPlayerAlive)
        {
            anim.SetBool("isDead", true);
        }
        else
        {
            anim.SetBool("isDead", false);
        }

        if (Input.GetButtonDown(pc.pInput.jumpButton) || Input.GetAxis(pc.pInput.rVertical) <= -0.5f)
        {
            if (pc.canDoubleJump || pc.canJump)
            {
                anim.SetTrigger("pressedJump");
            }
        }

        if (pc.jumpPressRemember > 0 && pc.canJump)
        {
            anim.SetTrigger("pressedJump");
        }
    }
}
