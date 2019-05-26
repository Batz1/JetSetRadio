using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDetection : MonoBehaviour
{
    public PlayerController co;
    float rbGravity = 2;

    [Range(0, 90)] public float minAngle;
    [Range(0, 90)] public float maxAngle;

    public float minDistance;
    public Vector2 feetPosition;

    [Header("FOR VIEW ONLY! DO NOT EDIT")]
    [SerializeField] Vector2 hitNormal = Vector2.zero;
    [SerializeField] float currentAngle = 0;
    [SerializeField] float currentGravity = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbGravity = co.rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        currentGravity = co.rb.gravityScale;

        RaycastHit2D[] hits = new RaycastHit2D[2];
        int h = Physics2D.RaycastNonAlloc(transform.position, -Vector2.up, hits);

        if (h > 1 && hits[1].distance <= minDistance)
        {
            hitNormal = hits[1].normal;

            // Getting the angle
            currentAngle = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y) * Mathf.Rad2Deg);
        }
        else
        {
            currentAngle = 0;
        }

        if (currentAngle > minAngle && currentAngle < maxAngle && !co.canWallJumpL && !co.canWallJumpR)
        {
            if (co.rb.velocity.y >= 0)
            {
                co.rb.gravityScale = rbGravity * (1 - (currentAngle * 0.0125f));
            }

            if (!co.gCheck.isGrounded && co.collidedWithAnything)
            {
                co.canDoubleJump = false;
                co.gCheck.isGrounded = true;
            }
            else if (!co.collidedWithAnything)
            {
                co.gCheck.isGrounded = false;
            }
        }
        else
        {
            co.rb.gravityScale = rbGravity;
        }
    }
}
