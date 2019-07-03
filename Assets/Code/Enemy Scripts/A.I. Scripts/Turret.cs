using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    Transform playerT;
    [SerializeField] string playerTag = "Player";
    public float range = 10f;
    public float turretSpeed = 5f;
    public float lockOnTimer;
    public Transform turret;
    public float timeToFire = 1f;
    private float fireCountdown = 0f; // start at zero so we can start firing right away
    public GameObject laser;
    public Transform firePoint;

    bool clearToEngage;

    // Start is called before the first frame update
    void Start()
    {
        fireCountdown = timeToFire;
        playerT = GameManager.instance.currentPlayer.transform;
        InvokeRepeating("UpdateTarget", 0f, lockOnTimer); // calls the UpdateTarget method 2 times in a second
    }

    void UpdateTarget()
    {
        target = playerT.position;
        float distanceToEngage = range;
        float distanceToPlayer = Vector2.Distance(transform.position, target);

        if(distanceToPlayer <= distanceToEngage)
        {
            Debug.Log(distanceToPlayer);
            clearToEngage = true;
        }
        else
        {
            clearToEngage = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        

        if(clearToEngage)
        {
            Vector3 dir = target - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir); //rotation
            Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * turretSpeed).eulerAngles;
            turret.rotation = Quaternion.Euler(0f, 0f, rotation.z);

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            fireCountdown = timeToFire;
        }

        if(fireCountdown <= 0)
        {
            laser.SetActive(true);
        }
        else
        {
            laser.SetActive(false);
        }
    }


    // draw the range if the target is selected
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
