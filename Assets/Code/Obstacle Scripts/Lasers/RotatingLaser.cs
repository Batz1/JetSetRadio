using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLaser : MonoBehaviour
{
    float realRotation;

    public float rightAngle;
    public float downAngle;
    public float leftAngle;
    public float upAngle;

    public float speed;
    public float cooldownTimer;

    bool canRotateRight;
    bool canRotateDown;
    bool canRotateLeft;
    bool canRotateUp;

    private void Awake()
    {
        canRotateRight = false;
        canRotateDown = false;
        canRotateLeft = false;
        canRotateUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ImplementRotation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,realRotation);
        UpdateRotation();
    }

    void Rotate(float start, float end)
    {
        realRotation = Mathf.MoveTowardsAngle(start, end, speed * Time.deltaTime);
    }

    void UpdateRotation()
    {
        if (canRotateRight)
        {
            Rotate(transform.eulerAngles.z, rightAngle);
        }
        else if (canRotateDown)
        {
            Rotate(transform.eulerAngles.z, downAngle);
        }
        else if (canRotateLeft)
        {
            Rotate(transform.eulerAngles.z, leftAngle);
        }
        else if (canRotateUp)
        {
            Rotate(transform.eulerAngles.z, upAngle);
        }
    }

    IEnumerator ImplementRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownTimer);
            canRotateUp = false;
            canRotateRight = true;
            yield return new WaitForSeconds(cooldownTimer);
            canRotateRight = false;
            canRotateDown = true;
            yield return new WaitForSeconds(cooldownTimer);
            canRotateDown = false;
            canRotateLeft = true;
            yield return new WaitForSeconds(cooldownTimer);
            canRotateLeft = false;
            canRotateUp = true;
        }
    }
}
