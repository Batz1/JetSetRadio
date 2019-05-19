using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = true;
    public bool cancelMomentum = false;
    public float groundDetectRemember;
    public float groundDetectTimer = 1;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if(groundDetectRemember <= 0)
            {
                cancelMomentum = true;
            }
            else
            {
                cancelMomentum = false;
                groundDetectRemember -= Time.deltaTime;
            }
        }
        else
        {
            cancelMomentum = false;
            groundDetectRemember = groundDetectTimer;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == col)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
