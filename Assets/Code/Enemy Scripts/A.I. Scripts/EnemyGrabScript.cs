using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabScript : MonoBehaviour
{
    private bool _playerPhysicIsStatic = false; //
    [SerializeField] private PlayerController playerScript;
    private int dashCount = 0;

    void Update()
    {
        if(_playerPhysicIsStatic)
        {
            playerScript.GetComponent<PlayerController>().canDashLeft = true;
            playerScript.GetComponent<PlayerController>().canDashRight = true;

            if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
            {
                dashCount++;
                Debug.Log(dashCount);
                if(dashCount>=5)
                {
                    PlayerController _playerPhysic = FindObjectOfType<PlayerController>();
                    _playerPhysic.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    _playerPhysicIsStatic = false;
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController _playerPhysic = FindObjectOfType<PlayerController>();
            _playerPhysic.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _playerPhysicIsStatic = true;
        }
    }
}
