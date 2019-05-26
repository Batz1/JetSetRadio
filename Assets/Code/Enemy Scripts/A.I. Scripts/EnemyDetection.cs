using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    private Transform target;
    private float timeBtwShots;
    public Vector3 newTargetPos;
    public float startTimeBtwShots;
    public GameObject projectile;


    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        newTargetPos = new Vector3(target.transform.position.x, -2.49f, target.transform.position.z);
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, newTargetPos, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position,target.position)<retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, newTargetPos, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}