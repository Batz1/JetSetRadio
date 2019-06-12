using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    //public GameObject pickUpEffect;
    [SerializeField] float pickupDuration = 5f;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           StartCoroutine( PowerUpPicked(collision));
        }
            
    }

    IEnumerator PowerUpPicked(Collider2D player)
    {
        //Instantiate(pickUpEffect, transform.position, transform.rotation);
        PlayerController jumpSpeedX = player.GetComponent<PlayerController>();
        jumpSpeedX.jumpSpeed += 3;
        Debug.Log("Power Up picked");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;


        yield return new WaitForSeconds(pickupDuration);
        jumpSpeedX.jumpSpeed -= 3;
        Destroy(this.gameObject);
    }
}
