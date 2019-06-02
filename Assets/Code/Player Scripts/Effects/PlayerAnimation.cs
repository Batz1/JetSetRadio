using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerController pc;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GameManager.instance.currentPlayer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("xVelocity", pc.xVelocityCheck);
        anim.SetBool("isGrounded", pc.gCheck.isGrounded);

        if (!GameManager.instance.isPlayerAlive)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
    }
}
