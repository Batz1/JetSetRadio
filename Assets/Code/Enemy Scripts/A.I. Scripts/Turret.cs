using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Vector3 target;
    Transform playerT;
    [SerializeField] Animator exclamationMark;
    [SerializeField] GameObject ExclamationMark;

    public float range = 10f;
    public float turretSpeed = 5f;
    public float lockOnTimer;
    public Transform turret;
    public float timeToFire = 1f;
    private float fireCountdown = 0f; // start at zero so we can start firing right away
    public GameObject laser;

    bool clearToEngage;

    // Start is called before the first frame update
    void Start()
    {
        fireCountdown = timeToFire;
        playerT = GameManager.instance.currentPlayer.transform;
        InvokeRepeating("UpdateTarget", 0f, lockOnTimer); // calls the UpdateTarget method N times in a second
        ExclamationMark.SetActive(false);
    }

    void UpdateTarget()
    {
        target = playerT.position;

        float distanceToEngage = range;
        float distanceToPlayer = Vector2.Distance(transform.position, target);

        if(distanceToPlayer <= distanceToEngage)
        {
            ExclamationMark.SetActive(true);
            exclamationMark.SetTrigger("IsPlayerInRadius");
            Debug.Log(distanceToPlayer);
            clearToEngage = true;
        }
        else
        {
            clearToEngage = false;
            ExclamationMark.SetActive(false);
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
            Fire();
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

    void Fire()
    {
        Vector3 dir = target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir); //rotation
        Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * turretSpeed).eulerAngles;
        turret.rotation = Quaternion.Euler(0f, 0f, rotation.z);

        fireCountdown -= Time.deltaTime;
    }

    // draw the range if the target is selected
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
