using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The platform will break if the player stand on it for x amount of seconds
/// 
/// </summary>
public class BreakablePlatformScript : MonoBehaviour
{

    //private GameObject breakablePlatform;
    private Rigidbody2D platformRB2D;
    [SerializeField] private float timerToBreak = 3f;

    // Start is called before the first frame update
    void Start()
    {
        platformRB2D = GetComponent<Rigidbody2D>();
        platformRB2D.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        Debug.Log(timerToBreak);
    }

    void OnCollisionStay2D(Collision2D collision)
    {        
        if(collision.gameObject.CompareTag("Player"))
        {
            timerToBreak--;
            if (timerToBreak<=0)
            {
                //Destroy(this.gameObject);
                platformRB2D.bodyType = RigidbodyType2D.Dynamic;
                timerToBreak = 0;
            }
        }
    }
}
