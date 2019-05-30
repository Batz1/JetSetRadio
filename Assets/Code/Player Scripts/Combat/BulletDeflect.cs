using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeflect : MonoBehaviour
{
    public PlayerController co;
    public Collider2D colL;
    public Collider2D colR;

    public ParticleSystem burstL;
    public ParticleSystem burstR;

    public string projectileTag;

    float[] timeTick;
    public float timeTickMax;

    // Start is called before the first frame update
    void Awake()
    {
        timeTick = new float[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTick[0] >= 0)
        {
            timeTick[0] -= Time.deltaTime;
        }
        else
        {
            colR.enabled = false;
        }

        if (timeTick[1] >= 0)
        {
            timeTick[1] -= Time.deltaTime;
        }
        else
        {
            colL.enabled = false;
        }

        if (co.isDashingL)
        {
            colR.enabled = true;
            colL.enabled = false;
            burstL.Play();
            timeTick[0] = timeTickMax;
        }

        if (co.isDashingR)
        {
            colL.enabled = true;
            colR.enabled = false;
            burstR.Play();
            timeTick[1] = timeTickMax;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            collision.gameObject.tag = "playerProjectile";
            Projectile pj = collision.gameObject.GetComponent<Projectile>();
            if (pj.parentEnemy != null)
            {
                pj.goTo = pj.parentEnemy.position - collision.gameObject.transform.position;
            }
            else
            {
                pj.goTo = -pj.goTo;
            }
        }
    }
}
