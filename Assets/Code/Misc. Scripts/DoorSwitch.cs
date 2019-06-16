using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public bool isPressed;
    float pressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        pressed = transform.localScale.y - 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            transform.localScale = new Vector3(transform.localScale.x, pressed, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPressed = true;
        }
    }
}
