using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float raycastDistance;
    private bool movingRight = true;
    public float MaxSpeed;
    public float speedAdd;
    public Transform groundDetection; // Raycast ground detection

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, raycastDistance); // this line of code shoots the raycast from the groundDetection variable downwards
        if(groundInfo.collider == false) // turns the enemy the opposite side
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if(speed >= MaxSpeed)
        {
            speed = MaxSpeed;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHitDetection phd = GameManager.instance.phd;
            phd.GetHit(collision.collider);
        }
        else if(collision.gameObject.CompareTag("Floor") == false)
        {
            Destroy(collision.gameObject);
            speed += speedAdd;

            if (speed >= MaxSpeed)
            {
                speed = MaxSpeed;
            }
        }
    }
}
