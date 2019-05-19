using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashParticles : MonoBehaviour
{
    public ParticleSystem dashLeft;
    public ParticleSystem dashRight;

    public PlayerController controller;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.gCheck.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.A) && controller.canDashLeft && !controller.canWallJumpL)
            {
                dashLeft.Play();
            }
            else if (Input.GetKeyDown(KeyCode.D) && controller.canDashRight && !controller.canWallJumpR)
            {
                dashRight.Play();
            }
        }
    }
}

