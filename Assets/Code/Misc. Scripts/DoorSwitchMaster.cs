using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchMaster : MonoBehaviour
{
    EndGoal e;
    public DoorSwitch[] switches;

    public bool CheckPress()
    {
        foreach (DoorSwitch s in switches)
        {
            if (!s.isPressed)
            {
                return false;
            }
        }

        return true;
    }

    private void Awake()
    {
        e = GetComponent<EndGoal>();
    }

    // Start is called before the first frame update
    void Start()
    {
        e.isLocked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CheckPress())
            {
                e.isLocked = false;
            }
        }
    }
}
