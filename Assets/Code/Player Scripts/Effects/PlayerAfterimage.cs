using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    public PlayerController controller;
    ParticleSystem afterimage;

    // Start is called before the first frame update
    void Start()
    {
        afterimage = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(controller.playerSprite.transform.localScale.x, 2);

        if (controller.rb.velocity.x > controller.speedLimit + 0.5f || controller.rb.velocity.x < -controller.speedLimit - 0.5f)
        {
            afterimage.Play();
        }
        else
        {
            afterimage.Stop();
        }
    }
}
