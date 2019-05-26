using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource aSource;

    public AudioClip jumpSound;
    public bool canPlayJ;
    public AudioClip dashSound;
    public bool canPlayD;
    public AudioClip landSound;
    public bool canPlayL;
    public AudioClip stompSound;
    public bool canPlaySA;
    public AudioClip sLandSound;
    public bool canPlaySL;


    public PlayerController co;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (co.gCheck.isGrounded)
        {
            canPlayJ = true;
            canPlayD = true;
            canPlaySA = true;
        }

        if (co.collidedWithAnything && (co.canWallJumpL || co.canWallJumpR) && !co.isSameWall)
        {
            canPlayD = true;
            canPlayJ = true;
        }
        else if (!co.collidedWithAnything)
        {
            canPlayL = true;
        }

        if (!co.gCheck.isGrounded)
        {
            if ((((Input.GetAxisRaw("Horizontal") < 0f || (Input.GetAxisRaw("Horizontal") == 0f && co.rb.velocity.x < 0)) && Input.GetButtonDown(co.pInput.dashButton)) || Input.GetAxis(co.pInput.rHorizontal) >= 0.5f) && co.canDashLeft && canPlayD)
            {
                aSource.PlayOneShot(dashSound);
                canPlayD = false;
            }
            else if ((((Input.GetAxisRaw("Horizontal") > 0f || (Input.GetAxisRaw("Horizontal") == 0f && co.rb.velocity.x > 0)) && Input.GetButtonDown(co.pInput.dashButton)) || Input.GetAxis(co.pInput.rHorizontal) <= -0.5f) && co.canDashRight && canPlayD)
            {
                aSource.PlayOneShot(dashSound);
                canPlayD = false;
            }
        }

        if ((co.jumpPressRemember > 0 && co.canJump && canPlayJ) || (Input.GetButtonDown(co.pInput.jumpButton) || Input.GetAxis(co.pInput.rVertical) <= -0.5f) && (co.canJump || co.canDoubleJump || ((co.canWallJumpL || co.canWallJumpR) && !co.isSameWall)) && canPlayJ)
        {
            aSource.PlayOneShot(jumpSound);
            canPlayJ = false;
        }

        if (co.collidedWithAnything)
        {
            if (co.hasDashedDown && canPlaySL)
            {
                aSource.PlayOneShot(sLandSound);
                canPlaySL = false;
            }
            else if (canPlayL)
            {
                aSource.PlayOneShot(landSound);
                canPlayL = false;
            }
        }

        if ((Input.GetAxis(co.pInput.rVertical) >= 0.5f || Input.GetButtonDown(co.pInput.stompButton)) && co.canDashDown && canPlaySA)
        {
            aSource.PlayOneShot(stompSound);
            canPlaySA = false;
            canPlaySL = true;
        }
    }

    public void PlaySoundOnAnimation(AudioClip clip)
    {
        aSource.PlayOneShot(clip);
    }
}
