﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float jumpForce;

    private Vector3 startPos;
    public Transform target;
    public float speed;
    public float upSpeed;
    public float downSpeed;
    private bool moveUp;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
       float step = speed * Time.deltaTime;
         if (transform.position == target.position)
         {
             moveUp = false;
         }
         else if (transform.position == startPos)
         {
             moveUp = true;
            speed = upSpeed;
         }
         if(moveUp == false)
         {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            speed = downSpeed;
        }
        else if (moveUp)
         {
             transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            
         }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.rigidbody.AddForce(Vector3.up * jumpForce);
        Debug.Log("FLY MY BOI");
        collision.gameObject.transform.SetParent(null);
    }
}
