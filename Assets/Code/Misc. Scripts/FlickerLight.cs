using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] float duration;
    Color redColor = Color.red;
    Color greenColor = Color.green;
    Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        float _time = Mathf.PingPong(Time.time, duration) / duration;
        light.color = Color.Lerp(redColor, greenColor, _time);
    }
}
