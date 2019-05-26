using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Put axes name here")]
    public string dashButton;
    public string jumpButton;
    public string stompButton;
    public string rHorizontal;
    public string rVertical;

    public float leftStickHoriz;
    public float leftStickVert;
    public float rightStickHoriz;
    public float rightStickVert;

    private void Update()
    {
        leftStickHoriz = Input.GetAxis("Horizontal");
        leftStickVert = Input.GetAxis("Vertical");
        rightStickHoriz = Input.GetAxis(rHorizontal);
        rightStickVert = Input.GetAxis(rVertical);
    }
}
