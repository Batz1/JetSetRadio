using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabScript : MonoBehaviour
{

    [SerializeField] private PlayerController playerScript;
    [SerializeField] float rotateSpeed = 0.5f;
    CamShake cs;
    private int dashCount = 0;
    public int dashLimit;
    private bool _playerPhysicIsStatic = false;

    public float ampGain;
    public float freqGain;
    public float duration;

    void Start()
    {
        cs = GameManager.instance.cs;
    }

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

                if(dashCount>=dashLimit)
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
            StartCoroutine(cs.Shake(ampGain, freqGain, duration));
        }
    }
}