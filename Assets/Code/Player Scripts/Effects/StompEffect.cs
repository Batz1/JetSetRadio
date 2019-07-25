using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEffect : MonoBehaviour
{
    public PlayerController pc;
    ParticleSystem downwardDash;
    public ParticleSystem secondary;
    public ParticleSystem landEffect;
    CamShake cs;

    public float ampGain, freqGain, duration;

    void Awake()
    {
        downwardDash = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cs = GameManager.instance.cs;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isDashingDown)
        {
            downwardDash.Play();
            secondary.Play();
            StartCoroutine(cs.Shake(ampGain, freqGain, duration));
        }

        if(pc.hasDashedDown && pc.gCheck.isGrounded)
        {
            landEffect.Play();
            StartCoroutine(cs.Shake(ampGain, freqGain, duration));
        }
    }
}
