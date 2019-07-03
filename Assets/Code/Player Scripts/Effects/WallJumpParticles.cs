using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpParticles : MonoBehaviour
{
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;

    public PlayerController co;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (co.canWallJumpL)
        {
            leftParticles.Play();
        }
        else
        {
            leftParticles.Stop();
        }

        if (co.canWallJumpR)
        {
            rightParticles.Play();
        }
        else
        {
            rightParticles.Stop();
        }
    }
}
