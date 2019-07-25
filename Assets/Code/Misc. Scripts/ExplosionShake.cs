using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShake : MonoBehaviour
{
    CamShake cs;
    public float ampGain, freqGain, duration;

    // Start is called before the first frame update
    void Start()
    {
        cs = GameManager.instance.cs;

        StartCoroutine(cs.Shake(ampGain, freqGain, duration));

        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
