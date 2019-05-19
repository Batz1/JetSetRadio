using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandEffect : MonoBehaviour
{
    ParticleSystem landEffect;
    public GroundCheck gCheck;

    bool canPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        landEffect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gCheck.isGrounded && canPlay)
        {
            landEffect.Play();
            canPlay = false;
        }

        if (!gCheck.isGrounded && !canPlay)
        {
            canPlay = true;
        }
    }
}
