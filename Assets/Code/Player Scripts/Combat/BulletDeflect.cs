using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeflect : MonoBehaviour
{
    public PlayerController co;
    public Collider2D colL;
    public Collider2D colR;

    CamShake cs;

    public ParticleSystem burstL;
    public ParticleSystem circleL;
    public ParticleSystem burstR;
    public ParticleSystem circleR;

    public string projectileTag;

    float[] timeTick;
    public float timeTickMax;

    public float amplitudeGain;
    public float frequencyGain;
    public float duration;


    void Awake()
    {
        timeTick = new float[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        cs = GameManager.instance.cs;
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
            //colR.enabled = false;
        }

        if (timeTick[1] >= 0)
        {
            timeTick[1] -= Time.deltaTime;
        }
        else
        {
           //colL.enabled = false;
        }

        if (co.isDashingL)
        {
            StartCoroutine(cs.Shake(amplitudeGain, frequencyGain, duration));
            //colR.enabled = true;
            //colL.enabled = false;
            burstL.Play();
            circleL.Play();
            timeTick[0] = timeTickMax;
        }

        if (co.isDashingR)
        {
            StartCoroutine(cs.Shake(amplitudeGain, frequencyGain, duration));
            //colL.enabled = true;
            //colR.enabled = false;
            burstR.Play();
            circleR.Play();
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
