using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEffect : MonoBehaviour
{
    ParticleSystem jumpParticles;
    public ParticleSystem secondary;
    public PlayerController co;
    public CamShake cs;

    bool canPlay;

    public float amplitudeGain;
    public float frequencyGain;
    public float duration;

    void Awake()
    {
        jumpParticles = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cs = GameManager.instance.cs;
    }

    // Update is called once per frame
    void Update()
    {
        if(co.isDoubleJumping && canPlay)
        {
            StartCoroutine(cs.Shake(amplitudeGain, frequencyGain, duration));
            jumpParticles.Play();
            secondary.Play();
            canPlay = false;
        }
        else if(co.gCheck.isGrounded && !canPlay)
        {
            canPlay = true;
        }
    }
}
